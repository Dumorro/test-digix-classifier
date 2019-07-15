﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digix.Raking.Domain.Core.Entities;

namespace Digix.Raking.Domain.Services.ScoreRules
{
    class FamilyDedependentScore : FamilyScoreBase
    {
        //TODO: change const to configurated values
        private const int FIRST_SCORE_VALUE = 3;
        private const int SECOND_SCORE_VALUE = 2;
        private const int PARAMETER_CLASSIFICATION_VALUE = 3;
        private const int AGE_LIMIT = 18;

        public FamilyDedependentScore(Family family) : base(family)
        {
        }

        protected override void CalculateScore()
        {
            int? totalUnder18 = base.family?.People.Count(p => IsUnderThen18(p));

            if (!totalUnder18.HasValue)
                base._isClassified = false;

            base._isClassified = true;

            if (totalUnder18.Value >= PARAMETER_CLASSIFICATION_VALUE)
                base._score = FIRST_SCORE_VALUE;

            if (totalUnder18.Value < PARAMETER_CLASSIFICATION_VALUE)
                base._score = SECOND_SCORE_VALUE;

        }

        private bool IsUnderThen18(Person person)
        {
            var age = DateTime.Now.Year - person.BirthDate.Year;

            return age < AGE_LIMIT;
        }
    }
}
