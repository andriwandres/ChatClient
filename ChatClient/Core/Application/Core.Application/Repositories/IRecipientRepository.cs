using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.Application.Repositories
{
    public interface IRecipientRepository
    {
        Task Add(Recipient recipient, CancellationToken cancellationToken = default);
    }
}
