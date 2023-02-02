namespace API.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IMessageRepository MessageRepository { get; }
    public ILikesRepository LikesRepository { get; }
    public IPhotoRepository PhotoRepository { get; }
    Task<bool> Complete();
    bool HasChanges();
}