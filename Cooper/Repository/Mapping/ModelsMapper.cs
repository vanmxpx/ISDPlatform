using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;
using Cooper.DAO.Models;
using Cooper.Controllers.ViewModels;
namespace Cooper.Repository.Mapping
{
    public class ModelsMapper
    {
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
            user_newType.PlatformLanguage = user.PlatformLanguage;
            user_newType.PlatformTheme = user.PlatformTheme;

            #endregion

            #region Transfering interop attributes
            //EMPTY

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
            user_newType.Email = user.Email;
            user_newType.IsVerified = user.IsVerified;
            user_newType.IsCreator = user.IsCreator;
            user_newType.IsBanned = user.IsBanned;
            user_newType.EndBanDate = user.EndBanDate;
            user_newType.PlatformLanguage = user.PlatformLanguage;
            user_newType.PlatformTheme = user.PlatformTheme;

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

            return user_newType;
        }

        public UserDb Map(UserRegistration user)
        {
            UserDb user_newType = new UserDb();

            user_newType.Email = user.Email;
            user_newType.Nickname = user.Nickname;
            user_newType.Password = user.Password;

            return user_newType;
        }


        #endregion
        
        #region Message Mapping

        public Message Map(MessageDb message)
        {
            Message message_newType = new Message();

            #region Transfer main attributes

            message_newType.Id = message.Id;
            message_newType.Content = message.Content;
            message_newType.CreateDate = message.CreateDate;
            message_newType.IsRead = message.IsRead;

            #endregion

            #region Transfering interop attributes

            message_newType.IdChat = new Chat() { Id = message.IdChat };
            message_newType.IdSender = new User() { Id = message.IdSender };

            #endregion

            return message_newType;
        }

        public MessageDb Map(Message message)
        {
            MessageDb message_newType = new MessageDb();

            #region Transfer main attributes

            message_newType.Id = message.Id;
            message_newType.Content = message.Content;
            message_newType.CreateDate = message.CreateDate;
            message_newType.IsRead = message.IsRead;


            #endregion

            #region Transfering interop attributes

            message_newType.IdChat = message.IdChat.Id;
            message_newType.IdSender = message.IdSender.Id;

            #endregion

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

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

            return chat_newType;
        }

        public ChatDb Map(Chat chat)
        {
            ChatDb chat_newType = new ChatDb();

            #region Transfer main attributes

            chat_newType.Id = chat.Id;
            chat_newType.ChatName = chat.ChatName;

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

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
            gameReview_newType.Date = gameReview.Date;
            gameReview_newType.Rating = gameReview.Rating;

            #endregion

            #region Transfering interop attributes

            gameReview_newType.IdReviewer = new User() { Id = gameReview.IdReviewer };
            gameReview_newType.IdGame = new Game() { Id = gameReview.GameId };

            #endregion

            return gameReview_newType;
        }

        public GameReviewDb Map(GameReview gameReview)
        {
            GameReviewDb gameReview_newType = new GameReviewDb();

            #region Transfer main attributes

            gameReview_newType.Id = gameReview.Id;
            gameReview_newType.Content = gameReview.Content;
            gameReview_newType.Date = gameReview.Date;
            gameReview_newType.Rating = gameReview.Rating;

            #endregion

            #region Transfering interop attributes

            gameReview_newType.IdReviewer = gameReview.IdReviewer.Id;
            gameReview_newType.IdGame = gameReview.Game.Id;

            #endregion

            return gameReview_newType;
        }

        #endregion

        #region Statistics Mapping
        private Statistics Map(StatisticsDb statistics)
        {
            Statistics statistics_newType = new Statistics();

            #region Transfer main attributes

            statistics_newType.Id = statistics.Id;
            statistics_newType.TimeSpent = statistics.TimeSpent;
            statistics_newType.RunsAmount = statistics.RunsAmount;
            statistics_newType.UserRecord = statistics.UserRecord;

            #endregion

            #region Transfering interop attributes

            statistics_newType.IdUser = new User() { Id = statistics.IdUser };
            statistics_newType.IdGame = new Game() { Id = statistics.IdGame };

            #endregion

            return statistics_newType;
        }

        private StatisticsDb Map(Statistics statistics)
        {
            StatisticsDb statistics_newType = new StatisticsDb();

            #region Transfer main attributes

            statistics_newType.Id = statistics.Id;
            statistics_newType.TimeSpent = statistics.TimeSpent;
            statistics_newType.RunsAmount = statistics.RunsAmount;
            statistics_newType.UserRecord = statistics.UserRecord;

            #endregion

            #region Transfering interop attributes

            statistics_newType.IdUser = statistics.IdUser;
            statistics_newType.IdGame = statistics.IdGame;

            #endregion

            return statistics_newType;
        }

        #endregion
    }
}
