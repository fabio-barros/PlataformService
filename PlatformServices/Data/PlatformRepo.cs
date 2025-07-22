using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Data;
using PlatformService.Data.Interfaces;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo(AppDbContext context) : IPlatformRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return [.. _context.Platforms];
        }

        public Platform GetPlatformById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");

            return _context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public void CreatePlatform(Platform platform)
        {
            ArgumentNullException.ThrowIfNull(platform);

            _context.Platforms.Add(platform);
        }

        public void UpdatePlatform(Platform platform)
        {
            ArgumentNullException.ThrowIfNull(platform);

            var existingPlatform = _context.Platforms.FirstOrDefault(p => p.Id == platform.Id) ?? throw new InvalidOperationException($"Platform with Id {platform.Id} not found.");

            // Update properties as needed
            existingPlatform.Name = platform.Name;
            existingPlatform.Publisher = platform.Publisher;
            existingPlatform.Cost = platform.Cost;
        }

        public void DeletePlatform(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");

            var platform = _context.Platforms.FirstOrDefault(p => p.Id == id) ?? throw new InvalidOperationException($"Platform with Id {id} not found.");
            _context.Platforms.Remove(platform);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}