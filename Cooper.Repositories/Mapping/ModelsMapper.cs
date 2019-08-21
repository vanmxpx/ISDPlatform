using Cooper.Controllers.ViewModels;
using Cooper.DAO.Models;
using Cooper.Models;
using System.Collections.Generic;

namespace Cooper.Repositories.Mapping
{
    public class ModelsMapper
    {
        public VerificationDb Map(Verification verify)
        {
            VerificationDb verify_newType = new VerificationDb();

            verify_newType.Email = verify.Email;
            verify_newType.EndVerifyDate = verify.EndVerifyDate;
            verify_newType.Token = verify.Token;

            return verify_newType;
        }
      
        public ResetTokenDb Map(ResetToken resetToken)
        {
            ResetTokenDb resetTokenDb = new ResetTokenDb();

            resetTokenDb.Email = resetToken.Email;
            resetTokenDb.Token = resetToken.Token;

            return resetTokenDb;
        }

        #region Game Mapping

        public Game Map(GameDb game)
        {
            Game game_newType = new Game();

            #region Transfer main attributes

            game_newType.Id = game.Id;
            game_newType.Name = game.Name;
            game_newType.Description = game.Description;
            game_newType.Genre = game.Genre;
            game_newType.Link = game.Link;
            game_newType.LogoURL = game.LogoURL;
            game_newType.CoverURL = game.CoverURL;
            game_newType.IsVerified = game.IsVerified;

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

            return game_newType;
        }

        public GameDb Map(Game game)
        {
            GameDb game_newType = new GameDb();

            #region Transfer main attributes

            game_newType.Id = game.Id;
            game_newType.Name = game.Name;
            game_newType.Description = game.Description;
            game_newType.Genre = game.Genre;
            game_newType.Link = game.Link;
            game_newType.LogoURL = game.LogoURL;
            game_newType.CoverURL = game.CoverURL;
            game_newType.IsVerified = game.IsVerified;

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

            return game_newType;
        }

        #endregion
        
        #region User Mapping
        public User Map(UserDb user)
        {
            User user_newType = new User();

            #region Transfer main attributes

            user_newType.Id = user.Id;
            user_newType.Name = user.Name;
            user_newType.Nickname = user.Nickname;
            user_newType.PhotoURL = user.PhotoURL;
            user_newType.Email = user.Email;
            user_newType.IsVerified = user.IsVerified;
            user_newType.IsCreator = user.IsCreator;
            user_newType.IsBanned = user.IsBanned;
            user_newType.EndBanDate = user.EndBanDate;
            user_newType.Description = user.Description;
            user_newType.PlatformLanguage = user.PlatformLanguage;
            user_newType.PlatformTheme = user.PlatformTheme;

            #endregion

            #region Transfering interop attributes
            

            #endregion

            return user_newType;
        }

        public UserDb Map(User user)
        {
            UserDb user_newType = new UserDb();

            #region Transfer main attributes

            user_newType.Id = user.Id;
            user_newType.Name = user.Name;
            user_newType.Nickname = user.Nickname;
            user_newType.PhotoURL = user.PhotoURL;
            user_newType.Description = user.Description;
            user_newType.Email = user.Email;
            user_newType.IsVerified = user.IsVerified;
            user_newType.IsCreator = user.IsCreator;
            user_newType.IsBanned = user.IsBanned;
            user_newType.EndBanDate = user.EndBanDate;
            user_newType.PlatformLanguage = user.PlatformLanguage;
            user_newType.PlatformTheme = user.PlatformTheme;

            #endregion
            

            return user_newType;
        }

        public UserDb Map(UserRegistration user)
        {
            UserDb user_newType = new UserDb();

            user_newType.Name = user.Name;
            user_newType.Email = user.Email;
            user_newType.Nickname = user.Nickname;
            user_newType.Password = user.Password;
            user_newType.PhotoURL = user.PhotoURL;

            return user_newType;
        }


        #endregion

        #region UserConnection Mapping

        public UsersConnection Map(UsersConnectionDb userConnection)
        {
            UsersConnection userConnection_newType = new UsersConnection();

            #region Transfer main attributes

            userConnection_newType.Id = userConnection.Id;
            userConnection_newType.User1 = new User() { Id = userConnection.IdUser1 };
            userConnection_newType.User2 = new User() { Id = userConnection.IdUser2 };
            userConnection_newType.AreFriends = userConnection.AreFriends;
            userConnection_newType.BlackListed = userConnection.BlackListed;

            #endregion

            return userConnection_newType;
        }

        public UsersConnectionDb Map(UsersConnection userConnection)
        {
            UsersConnectionDb userConnection_newType = new UsersConnectionDb();

            #region Transfer main attributes

            userConnection_newType.Id = userConnection.Id;
            userConnection_newType.IdUser1 = userConnection.User1.Id;
            userConnection_newType.IdUser2 = userConnection.User2.Id;
            userConnection_newType.AreFriends = userConnection.AreFriends;
            userConnection_newType.BlackListed = userConnection.BlackListed;


            #endregion

            return userConnection_newType;
        }

        #endregion

        #region Message Mapping

        public Message Map(MessageDb message)
        {
            Message message_newType = new Message();
 
            message_newType.Id = message.Id;
            message_newType.Content = message.Content;
            message_newType.CreateDate = message.CreateDate;
            message_newType.IsRead = message.IsRead;

            message_newType.ChatId = message.ChatId;
            message_newType.SenderId = message.SenderId;
            

            return message_newType;
        }

        public MessageDb Map(Message message)
        {
            MessageDb message_newType = new MessageDb();
            
            message_newType.Id = message.Id;
            message_newType.Content = message.Content;
            message_newType.CreateDate = message.CreateDate;
            message_newType.IsRead = message.IsRead;
            
            message_newType.ChatId = message.ChatId;
            message_newType.SenderId = message.SenderId;
            

            return message_newType;
        }

        #endregion
        
        #region UserReview Mapping

        public UserReview Map(UserReviewDb userReview)
        {
            UserReview userReview_newType = new UserReview();

            #region Transfer main attributes

            userReview_newType.Id = userReview.Id;
            userReview_newType.Content = userReview.Content;
            userReview_newType.CreateDate = userReview.CreateDate;
            userReview_newType.Rating = userReview.Rating;

            #endregion

            #region Transfering interop attributes

            userReview_newType.IdReviewer = new User() { Id = userReview.IdReviewer };
            userReview_newType.IdReviewed = new User() { Id = userReview.IdReviewed };

            #endregion

            return userReview_newType;
        }

        public UserReviewDb Map(UserReview userReview)
        {
            UserReviewDb userReview_newType = new UserReviewDb();

            #region Transfer main attributes

            userReview_newType.Id = userReview.Id;
            userReview_newType.Content = userReview.Content;
            userReview_newType.CreateDate = userReview.CreateDate;
            userReview_newType.Rating = userReview.Rating;

            #endregion

            #region Transfering interop attributes

            userReview_newType.IdReviewer = userReview.IdReviewer.Id;
            userReview_newType.IdReviewed = userReview.IdReviewed.Id;

            #endregion

            return userReview_newType;
        }

        #endregion
        
        #region Chat Mapping
        public Chat Map(ChatDb chat)
        {
            Chat chat_newType = new Chat();

            #region Transfer main attributes

            chat_newType.Id = chat.Id;
            chat_newType.ChatName = chat.ChatName;
            chat_newType.IsOnetoOneChat = chat.IsOneToOneChat;
            #endregion

            chat_newType.Participants = new List<User>(capacity: chat.Participants.Count);

            foreach (var userId in chat.Participants)
            {
                User user = new User() { Id = userId };
                chat_newType.Participants.Add(user);
            }

            return chat_newType;
        }

        public ChatDb Map(Chat chat)
        {
            ChatDb chat_newType = new ChatDb();
            

            chat_newType.Id = chat.Id;
            chat_newType.ChatName = chat.ChatName;
            chat_newType.IsOneToOneChat = chat.IsOnetoOneChat;

            if (chat.Participants != null)
            {
                chat_newType.Participants = new List<long>(capacity: chat.Participants.Count);

                foreach (var user in chat.Participants)
                {
                    chat_newType.Participants.Add(user.Id);
                }
            }

            return chat_newType;
        }

        #endregion

        #region Game Review Mapping
        public GameReview Map(GameReviewDb gameReview)
        {
            GameReview gameReview_newType = new GameReview();

            #region Transfer main attributes

            gameReview_newType.Id = gameReview.Id;
            gameReview_newType.Content = gameReview.Content;
            gameReview_newType.CreateDate = gameReview.CreateDate;
            gameReview_newType.Rating = gameReview.Rating;

            #endregion

            #region Transfering interop attributes

            gameReview_newType.Reviewer = new User() { Id = gameReview.IdReviewer };
            gameReview_newType.Game = new Game() { Id = gameReview.IdGame };

            #endregion

            return gameReview_newType;
        }

        public GameReviewDb Map(GameReview gameReview)
        {
            GameReviewDb gameReview_newType = new GameReviewDb();

            #region Transfer main attributes

            gameReview_newType.Id = gameReview.Id;
            gameReview_newType.Content = gameReview.Content;
            gameReview_newType.CreateDate = gameReview.CreateDate;
            gameReview_newType.Rating = gameReview.Rating;

            #endregion

            #region Transfering interop attributes

            gameReview_newType.IdReviewer = gameReview.Reviewer.Id;
            gameReview_newType.IdGame = gameReview.Game.Id;

            #endregion

            return gameReview_newType;
        }

        #endregion

        #region Statistics Mapping
        public Statistics Map(StatisticsDb statistics)
        {
            Statistics statistics_newType = new Statistics();

            #region Transfer main attributes

            statistics_newType.Id = statistics.Id;
            statistics_newType.TimeSpent = statistics.TimeSpent;
            statistics_newType.RunsAmount = statistics.RunsAmount;
            statistics_newType.UserRecord = statistics.UserRecord;

            #endregion

            #region Transfering interop attributes

            statistics_newType.User = new User() { Id = statistics.IdUser };
            statistics_newType.Game = new Game() { Id = statistics.IdGame };

            #endregion

            return statistics_newType;
        }

        public StatisticsDb Map(Statistics statistics)
        {
            StatisticsDb statistics_newType = new StatisticsDb();

            #region Transfer main attributes

            statistics_newType.Id = statistics.Id;
            statistics_newType.TimeSpent = statistics.TimeSpent;
            statistics_newType.RunsAmount = statistics.RunsAmount;
            statistics_newType.UserRecord = statistics.UserRecord;

            #endregion

            #region Transfering interop attributes

            statistics_newType.IdUser = statistics.User.Id;
            statistics_newType.IdGame = statistics.Game.Id;

            #endregion

            return statistics_newType;
        }

        #endregion
    }
}
