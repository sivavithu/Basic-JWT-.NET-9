﻿namespace JWT_Auth.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }
}