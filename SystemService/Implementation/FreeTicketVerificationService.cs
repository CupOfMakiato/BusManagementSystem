using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRepository.Interface;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class FreeTicketVerificationService : IFreeTicketVerificationService 
    {
        //private readonly IFreeTicketVerificationRepository _repository;

        //public FreeTicketVerificationService(IFreeTicketVerificationRepository repository)
        //{
        //    _repository = repository;
        //}

        //public List<FreeTicketVerification> GetAll()
        //{
        //    return _repository.GetAll();
        //}

        //public FreeTicketVerification GetVerificationById(int verificationId)
        //{
        //    return _repository.GetById(verificationId);
        //}

        //public void AddFreeTicketVerification(FreeTicketVerification verification)
        //{
        //    _repository.Add(verification);
        //}
    }
}
