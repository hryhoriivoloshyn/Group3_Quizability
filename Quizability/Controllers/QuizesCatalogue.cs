using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.Controllers
{
    public class QuizesCatalogue : Controller
    {
        private Quizability_serverContext db;
        public QuizesCatalogue(Quizability_serverContext context)
        {
            db = context;
        }
        private async Task<List<Quize>> GetQuizes()
        {
            var Quizes = new List<Quize>();
            var allQuizes = await db.Quizes.ToListAsync();
            if(allQuizes?.Any()==true)
            {
                foreach(var quiz in allQuizes)
                {
                    Quizes.Add(new Quize()
                    {
                        QuizId = quiz.QuizId,
                        QuizName = quiz.QuizName,
                        Description = quiz.Description,
                        Rating = quiz.Rating,
                        QuizTime = quiz.QuizTime,
                        Popular = quiz.Popular,
                        Difficulty = quiz.Difficulty,
                        TopicId = quiz.TopicId,
                        ImageSrc = quiz.ImageSrc
                    });
                }
            }
            return Quizes;
        }
        public async Task<List<Topic>> GetTopics()
        {
            var topics = new List<Topic>();
            var allTopics = await db.Topics.ToListAsync();
            var quizesList = await GetQuizes();
            var tempList = new List<Quize>();
            if (allTopics?.Any()==true)
            {
                foreach(var topic in allTopics)
                {
                    foreach(var quiz in quizesList)
                    {
                        if(quiz.TopicId==topic.TopicId)
                        {
                            tempList.Add(quiz);
                        }
                    }
                    topics.Add(new Topic()
                    {
                        TopicId = topic.TopicId,
                        TopicName = topic.TopicName,
                        Quizes = tempList
                    });
                }
                
            }
            return topics;
        }
        public async Task<ViewResult> GetAllQuizes()
        {
            var data = await GetTopics();
            return View(data);
        }
    }
}
