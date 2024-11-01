﻿using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.ModelMappers
{

    public static class EducationMapper
    {
        public static Education CreateFrom(this Education source)
        {
            return new Education
            {
                EducationId = source.EducationId,
                Title = source.Title
            };
        }
    }
}