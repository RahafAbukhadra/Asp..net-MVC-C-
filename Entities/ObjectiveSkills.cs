﻿namespace FinalProject.Entities
{ 
    public class ObjectiveSkills
    {
        public Objective objective { get; set; }
        public int objectiveId { get; set; }

        public Skill skill { get; set; }
        public int skillId { get; set; }
    }
}
