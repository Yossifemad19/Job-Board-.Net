﻿using Core.Enums;

namespace Api.DTOs.CompanyDtos
{
    public class GetCompanyDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CompanySize CompanySize { get; set; }
        public string Email { get; set; }
    }
}
