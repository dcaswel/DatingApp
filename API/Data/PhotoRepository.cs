using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class PhotoRepository : IPhotoRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PhotoRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos()
    {
        return await _context.Photos
            .IgnoreQueryFilters()
            .Where(p => p.IsApproved == false)
            .Include(p => p.AppUser)
            .ProjectTo<PhotoForApprovalDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<Photo> GetPhotoByIdAsync(int id)
    {
        return await _context.Photos.IgnoreQueryFilters().Where(p => p.Id == id).FirstAsync();
    }

    public void RemovePhoto(Photo photo)
    {
        _context.Photos.Remove(photo);
    }

    public async Task<IEnumerable<Photo>> GetUserPhotosAsync(int userId)
    {
        return await _context.Photos.IgnoreQueryFilters().Where(p => p.AppUserId == userId).ToListAsync();
    }
}