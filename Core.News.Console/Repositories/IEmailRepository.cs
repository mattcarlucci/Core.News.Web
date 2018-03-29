using System;
using System.Collections.Generic;
using System.Linq;
using Core.News.Mail;
using Crypto.Compare.ViewModels;

namespace Core.News.Services
{
    public interface IEmailRepository
    {
        IEmailConfiguration CloneConfiguration(string schedule);
        List<EmailAddress> GetScheduleById(string schedule);
        List<IGrouping<string, EmailAddress>> GetSchedules();
        StoryViewModels GetStories(DateTime startDate);
        UserConfiguration GetUsers(string schedule);
        void SaveChanges();
        bool Enabled { get; }
    }
}