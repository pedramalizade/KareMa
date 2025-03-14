using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.DTOs.CustomerDTO;
using KareMa.Domain.Core.DTOs.Expert;
using KareMa.Domain.Core.DTOs.OrderDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly AppDbContext _context;
        public ExpertRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(ExpertCreateDto expertCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                // چک کردن تکراری بودن AppUserId
                var existingExpert = await _context.Experts
                    .AsNoTracking()
                    .AnyAsync(e => e.AppUserId == expertCreateDto.AppUserId && !e.IsDeleted, cancellationToken);
                if (existingExpert)
                {
                    Console.WriteLine($"Expert with AppUserId: {expertCreateDto.AppUserId} already exists.");
                    throw new InvalidOperationException($"متخصصی با AppUserId = {expertCreateDto.AppUserId} قبلاً ثبت شده است.");
                }

                var newModel = new Expert
                {
                    AppUserId = expertCreateDto.AppUserId,
                    FirstName = expertCreateDto.FirstName,
                    LastName = expertCreateDto.LastName,
                    Gender = expertCreateDto.Gender,
                    PhoneNumber = expertCreateDto.PhoneNumber,
                    Address = expertCreateDto.Address,
                    BankCardNumber = expertCreateDto.BankCardNumber,
                    Balance = expertCreateDto.Balance,
                    BirthDate = expertCreateDto.BirthDate,
                    Image = expertCreateDto.Image,
                    Services = expertCreateDto.Services != null
                        ? _context.Services.Where(s => expertCreateDto.Services.Contains(s.Id)).ToList()
                        : new List<Service>()
                };

                Console.WriteLine($"Creating expert with AppUserId: {newModel.AppUserId}, Services Count: {newModel.Services?.Count ?? 0}");
                await _context.Experts.AddAsync(newModel, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                Console.WriteLine("Expert saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExpertRepository.Create: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw; 
            }
        }

        public async Task<bool> Delete(int expertId, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Attempting to delete expert with ID: {expertId}");
            var targetModel = await FindExpert(expertId, cancellationToken);
            if (targetModel == null)
            {
                Console.WriteLine($"Expert with ID: {expertId} not found.");
                return false;
            }

            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken); 
            Console.WriteLine($"Expert with ID: {expertId} marked as deleted.");
            return true;
        }

        public async Task<List<Expert>> GetAll(CancellationToken cancellationToken)
        {
            Console.WriteLine("Fetching all experts...");
            var experts = await _context.Experts
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .ToListAsync(cancellationToken);

            Console.WriteLine($"Found {experts.Count} active experts.");
            return experts;
        }

        public async Task<Expert> GetById(int expertId, CancellationToken cancellationToken)
        {
            return await FindExpert(expertId, cancellationToken);
        }

        public async Task<bool> Update(ExpertUpdateDto expertUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await _context.Experts
                .Include(e => e.Services)
                .FirstOrDefaultAsync(e => e.Id == expertUpdateDto.Id && !e.IsDeleted, cancellationToken);

            if (targetModel == null)
            {
                Console.WriteLine($"Expert with ID {expertUpdateDto.Id} not found.");
                return false;
            }

            targetModel.FirstName = expertUpdateDto.FirstName;
            targetModel.LastName = expertUpdateDto.LastName;
            targetModel.PhoneNumber = expertUpdateDto.PhoneNumber;
            targetModel.Gender = expertUpdateDto.Gender;
            targetModel.BankCardNumber = expertUpdateDto.BankCardNumber;
            targetModel.Balance = expertUpdateDto.Balance;
            targetModel.BirthDate = expertUpdateDto.BirthDate;
            targetModel.Bio = expertUpdateDto.Bio;
            if (expertUpdateDto.Image != null)
                targetModel.Image = expertUpdateDto.Image;

            Console.WriteLine($"ServiceIds to save: {string.Join(", ", expertUpdateDto.ServiceIds ?? new List<int>())}");
            targetModel.Services ??= new List<Service>();
            targetModel.Services.Clear();
            if (expertUpdateDto.ServiceIds != null && expertUpdateDto.ServiceIds.Any())
            {
                var services = await _context.Services
                    .Where(s => expertUpdateDto.ServiceIds.Contains(s.Id))
                    .ToListAsync(cancellationToken);
                if (services.Count != expertUpdateDto.ServiceIds.Count)
                {
                    Console.WriteLine($"Some service IDs were not found: {string.Join(", ", expertUpdateDto.ServiceIds.Except(services.Select(s => s.Id)))}");
                    return false;
                }
                targetModel.Services.AddRange(services);
                Console.WriteLine($"Services assigned: {string.Join(", ", targetModel.Services.Select(s => s.Id))}");
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                Console.WriteLine($"Expert with ID {expertUpdateDto.Id} updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                throw new Exception("خطا در ذخیره تغییرات در دیتابیس", ex);
            }
        }

        public async Task<int> ExpertCount(CancellationToken cancellationToken)
        {
            var count = await _context.Experts.CountAsync(cancellationToken);
            return count;
        }

        public async Task<ExpertSummaryDto> GetExpertSummary(int id, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Fetching expert summary for ID: {id}");
            var target = await _context.Experts
                .Include(e => e.Services)
                .Include(e => e.Comments)
                .Where(e => e.Id == id && e.IsDeleted == false)
                .Select(e => new ExpertSummaryDto
                {
                    Id = e.Id,
                    Comments = (e.Comments != null ? e.Comments
                        .Where(c => c.IsAccept == true && c.IsDeleted == false)
                        .Select(x => new Comment
                        {
                            Customer = x.Customer,
                            Score = x.Score,
                            Title = x.Title,
                            Description = x.Description,
                            CreatedAt = x.CreatedAt,
                            IsAccept = x.IsAccept,
                            IsDeleted = x.IsDeleted
                        }).ToList() : new List<Comment>()),
                    FirstName = e.FirstName,
                    Gender = e.Gender,
                    LastName = e.LastName,
                    ProfileImage = e.Image,
                    Services = e.Services ?? new List<Service>(),
                    Balance = e.Balance 
                }).FirstOrDefaultAsync(cancellationToken);

            if (target == null)
            {
                Console.WriteLine($"Expert with ID: {id} not found or is deleted.");
                return new ExpertSummaryDto
                {
                    Id = id,
                    Comments = new List<Comment>(),
                    Services = new List<Service>(),
                    Balance = 0
                };
            }

            Console.WriteLine($"Expert Balance for ID: {id} is {target.Balance}");
            Console.WriteLine($"Expert Comments count for ID: {id} is {target.Comments.Count}");
            Console.WriteLine($"Expert Services count for ID: {id} is {target.Services.Count}");
            return target;
        }


        public async Task<int> ExpertCommentCount(int id, CancellationToken cancellationToken)
        {
            var targetExpert = await _context.Experts.Where(e => e.Id == id).SelectMany(e => e.Comments).CountAsync();
            return targetExpert;
        }
        public async Task<int> ExpertAverageScores(int id, CancellationToken cancellationToken)
        {
            var targetExpert = await _context.Experts.Include(o => o.Comments).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (targetExpert == null || targetExpert.Comments == null || !targetExpert.Comments.Any())
            {
                return 0;
            }
            var score = (int)targetExpert.Comments.Select(c => c.Score).Average();
            return score;
        }

        public async Task<int> ExpertOrderCount(int id, CancellationToken cancellationToken)
        {
            var targetExpertSuggestion = await _context.Experts.Where(e => e.Id == id).SelectMany(e => e.Suggestions).ToListAsync(cancellationToken);
            var suggestions = targetExpertSuggestion.Count(o => o.Status == StatusEnum.Done);
            return suggestions;
        }

        public async Task<List<int>> GetExpertServiceIds(int id, CancellationToken cancellationToken)
        {
            var expertServices = await _context.Experts.Where(e => e.Id == id).SelectMany(e => e.Services).ToListAsync(cancellationToken);
            var servicesId = expertServices.Select(s => s.Id).ToList();
            return servicesId;
        }

        public async Task<ExpertUpdateDto> ExpertUpdateInfo(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Experts.Include(e => e.Services)
                .Select(e => new ExpertUpdateDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    Gender = e.Gender,
                    Balance = e.Balance,
                    BirthDate = e.BirthDate,
                    BankCardNumber = e.BankCardNumber,
                    Image = e.Image,
                    Bio = e.Bio,
                    ServiceIds = e.Services
                        .Select(s => s.Id)
                        .ToList()
                }).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            Console.WriteLine($"ExpertUpdateInfo for ID {id}: ServiceIds = {string.Join(", ", result?.ServiceIds ?? new List<int>())}");
            return result;
        }

        public async Task<ExpertNameDto> GetExpertName(int id, CancellationToken cancellationToken)
        {
            var targetExpert = await _context.Experts.AsNoTracking().Where(e => e.Id == id)
                  .Select(e => new ExpertNameDto
                  {
                      FirstName = e.FirstName,
                      LastName = e.LastName,
                      Balance = e.Balance
                  })
                  .FirstOrDefaultAsync(cancellationToken);

            if (targetExpert == null)
                return new ExpertNameDto();

            return targetExpert;
        }
        public async Task<Expert> GetExpertById(int expertId, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Fetching expert with ID: {expertId}");
            var expert = await _context.Experts
                .FirstOrDefaultAsync(e => e.Id == expertId && !e.IsDeleted, cancellationToken);

            if (expert == null)
            {
                Console.WriteLine($"Expert with ID: {expertId} not found or is deleted.");
            }
            return expert;
        }

        public async Task UpdateBalance(int expertId, decimal newBalance, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Updating balance for Expert ID: {expertId} to {newBalance}");
            var expert = await _context.Experts
                .FirstOrDefaultAsync(e => e.Id == expertId && !e.IsDeleted, cancellationToken);

            if (expert == null)
            {
                Console.WriteLine($"Expert with ID: {expertId} not found or is deleted.");
                throw new Exception($"Expert with ID {expertId} not found or is deleted.");
            }

            expert.Balance = newBalance;
            await _context.SaveChangesAsync(cancellationToken);
            Console.WriteLine($"Balance updated successfully for Expert ID: {expertId}");
        }


        private async Task<Expert> FindExpert(int id, CancellationToken cancellationToken)
          => await _context.Experts.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    }
}

