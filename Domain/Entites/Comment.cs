using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Comment
    {
        public int Id { get;private set; }

        public DateTime DateOfRegisterComment { get; private set; } = DateTime.Now;

        public string Strength { get; private set; } = null!;

        public string WeakPoints { get; private set; } = null!;

        public string UserId { get; private set; }
        public string UserName { get; private set; }

        public Hotel Hotel { get; private set; } = null!;

        public int HotelId { get; private set; }

        public double AverageRateUser { get; private set; }

        public StatusComment StatusComment { get; private set; } = StatusComment.Waiting;
        public Comment()
        {

        }

        public Comment(int hotelId, string userId, string strength, string weakPoints, string userName, double averageRateUser)
        {
            HotelId = hotelId;
            UserId = userId;
            Strength = strength;
            WeakPoints = weakPoints;
            UserName = userName;
            AverageRateUser = averageRateUser;
        }

        public void ChangeStatusComment(StatusComment statusComment)
        {
            StatusComment = statusComment;
        }

    }

    public enum StatusComment
    {
        Waiting = 1,
        Accepted = 2,
        Rejected = 3
    }
}
