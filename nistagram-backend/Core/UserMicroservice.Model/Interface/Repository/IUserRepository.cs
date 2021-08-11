using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Model;

namespace UserMicroservice.Core.Interface.Repository
{
    public interface IUserRepository : IRepository<RegisteredUser>
    {
        public Maybe<RegisteredUser> GetByUsername(String username);

        public Maybe<User> GetRoleByUsername(String username);

        public IEnumerable<RegisteredUser> GetBy(string id, string name, string access);

        public void Follow(Guid id, Guid followedById, Guid followingId);

        public void FollowPrivate(Guid id, Guid followedById, Guid followingId);

        public Boolean AlreadyFollowing(Guid requests_follow_id, Guid recieves_follow_id);

        public Boolean AlreadyFollowingPrivate(Guid requests_follow_id, Guid recieves_follow_id);

        public void HandleFollowRequest(Guid id, String type, Boolean is_approved);

        public IEnumerable<FollowRequest> GetFollowRequestsForUser(Guid id);

        public IEnumerable<RegisteredUser> GetFollowing(Guid id);

        public void AddProfilePicture(Guid id, string image);

        public void AddCloseFriend(Guid id, Guid userId, Guid closeFriendId);

        public IEnumerable<RegisteredUser> GetFollowingWithoutMuted(Guid id);

        public void Mute(Guid id, Guid mutedById, Guid mutingId);

        public void Block(Guid id, Guid blockedById, Guid blockingId);

        public void DeleteFollows(Guid blockedById, Guid blockingId);

        public void DeleteFollowRequests(Guid blockedById, Guid blockingId);

        public Maybe<User> GetByIdWithoutBlocked(Guid loggedId, Guid userId);

        public IEnumerable<RegisteredUser> GetByWithoutBlocked(Guid loggedId, string name, string access);

        public RegisteredUser EditVerifiedUser(VerifiedUser registeredUser);

        public void BanUser(Guid id);

        public RegisteredUser EditAgent(Agent agent);

        public void DeleteFollow(Guid followedById, Guid followingId);

        public void DeleteMute(Guid id);

        public void DeleteBlock(Guid id);

        public Maybe<User> GetByIdWithType(Guid id);
    }
}