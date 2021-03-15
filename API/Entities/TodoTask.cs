using System;

namespace API.Entities
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public DateTime DueDate { get; set; }
        public int? StatusId { get; set; }
    }
}