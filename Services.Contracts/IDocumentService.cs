﻿using LMS.Shared.DTOs.Document;

namespace Services.Contracts
{
    public interface IDocumentService : IServiceBase<DocumentDto, DocumentCreateDto, DocumentUpdateDto>
    {
    }
}