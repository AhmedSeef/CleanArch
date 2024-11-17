﻿using CleanArch.Domain.Core.Commands;

namespace CleanArch.Domain.Commands
{
    public class CourseCommand : Command
    {
        public string Name { get; protected set; }
        public string Dsecription { get; protected set; }
        public string ImageUrl { get; protected set; }
    }
}
