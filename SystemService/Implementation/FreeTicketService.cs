using BusinessObject.Entity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using SystemRepository.Implementation;
using SystemRepository.Interface;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class FreeTicketService : IFreeTicketService
    {
        private readonly IFreeTicketRepository _freeTicketRepository;

        public FreeTicketService(IFreeTicketRepository freeTicketRepository)
        {
            _freeTicketRepository = freeTicketRepository;
        }

        public List<FreeTicket> GetAllFreeTickets() // Phương thức trả về danh sách FreeTicket
        {
            return _freeTicketRepository.GetAllFreeTickets(); // Gọi phương thức từ Repository
        }

        public FreeTicket GetFreeTicketById(int id) // Phương thức trả về FreeTicket theo ID
        {
            return _freeTicketRepository.GetFreeTicketById(id); // Gọi phương thức từ Repository
        }

        public  void AddFreeTicket(FreeTicket freeTicket)
        {
             _freeTicketRepository.AddFreeTicket(freeTicket);
        }


        public void UpdateFreeTicket(FreeTicket freeTicket) // Phương thức cập nhật FreeTicket
        {
            _freeTicketRepository.UpdateFreeTicket(freeTicket); // Gọi phương thức từ Repository
        }

        public void DeleteFreeTicket(int id) // Phương thức xóa FreeTicket theo ID
        {
            _freeTicketRepository.DeleteFreeTicket(id); // Gọi phương thức từ Repository
        }

        private readonly string _folderSaveImage = "wwwroot/uploads/freetickets";
        private readonly string _urlImage = "/uploads/freetickets/";

        // Save the image and return the relative URL for storing in the database
        public async Task<string> SaveTicketImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), _folderSaveImage);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save file: {ex.Message}");
                throw;
            }

            // Return the relative path to be saved in the database
            return $"{_urlImage}{uniqueFileName}";
        }


        // Retrieve an image by file name and return as a byte array
        public async Task<byte[]> GetTicketImageAsync(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _folderSaveImage, fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return null; // or throw new FileNotFoundException("File not found.");
            }

            try
            {
                return await System.IO.File.ReadAllBytesAsync(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return null;
            }
        }

        //public bool IsIdNumberExistingWithStatus(string idNumber, int status)
        //{
        //    return _freeTicketRepository.IsIdNumberExistingWithStatus(idNumber, status);
        //}
        public bool IsIdNumberExisting(string idNumber)
        {
            return _freeTicketRepository.IsIdNumberExisting(idNumber);
        }

        public void VerifyFreeTicket(FreeTicket freeTicket)
        {
            _freeTicketRepository.VerifyFreeTicket(freeTicket);
        }

        public async Task<Ticket> GetOrCreateTicketAsync(string idNumber)
        {
            return await _freeTicketRepository.GetOrCreateTicketAsync(idNumber);
        }
    }
}
