﻿namespace Squire.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;
    using System.Linq.Expressions;

    public class InlineValidator : IValidator
    {
        private readonly Func<IPlayer, string, bool> validator;

        public InlineValidator(Expression<Func<IPlayer, string, bool>> validatorExpression)
        {
            validator.VerifyParam("validator").IsNotNull();
            this.validator = validatorExpression.Compile();
        }

        public InlineValidator(Func<IPlayer, string, bool> validator)
        {
            validator.VerifyParam("validator").IsNotNull();
            this.validator = validator;
        }

        public bool Validate(IPlayer player, string attemptedHash)
        {
            return this.validator(player, attemptedHash);
        }
    }
}
