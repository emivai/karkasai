using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;
using HappyPaws.EmailService.Interfaces;
using HappyPaws.EmailService.Models;

namespace HappyPaws.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;

        public AppointmentService(IAppointmentRepository appointmentRepository, ITimeSlotRepository timeSlotRepository, IUserRepository userReposiory, IEmailSender emailSender)
        {
            _appointmentRepository = appointmentRepository;
            _timeSlotRepository = timeSlotRepository;
            _userRepository = userReposiory;
            _emailSender = emailSender;
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await ClaimTimeSlot(appointment);

            var result = await _appointmentRepository.AddAsync(appointment);
            var user = await _userRepository.GetAsync(result.UserId);

            await _emailSender.SendEmailAsync(CreateEmailMessage(result, user));

            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            var appointment = await GetAsync(id);

            await VaccateTimeSlot(appointment);

            await _appointmentRepository.DeleteAsync(id);
        }

        public async Task<List<Appointment>> GetAllAsync(Guid petId)
        {
            return await _appointmentRepository.GetAllAsync(petId);
        }

        public async Task<List<Appointment>> GetAllForDoctorAsync(Guid petId, Guid doctorId)
        {
            return await _appointmentRepository.GetAllForDoctorAsync(petId, doctorId);
        }

        public async Task<Appointment> GetAsync(Guid id)
        {
            return await _appointmentRepository.GetAsync(id);
        }

        public async Task<Appointment> UpdateAsync(Guid id, Appointment appointment)
        {
            return await _appointmentRepository.UpdateAsync(id, appointment);
        }

        private async Task ClaimTimeSlot(Appointment appointment)
        {
            var timeSlot = await _timeSlotRepository.GetAsync(appointment.TimeSlotId);

            timeSlot.Available = false;

            await _timeSlotRepository.UpdateAsync(appointment.TimeSlotId, timeSlot);
        }

        private async Task VaccateTimeSlot(Appointment appointment)
        {
            var timeSlot = await _timeSlotRepository.GetAsync(appointment.TimeSlotId);

            timeSlot.Available = true;

            await _timeSlotRepository.UpdateAsync(appointment.TimeSlotId, timeSlot);
        }

        private Message CreateEmailMessage(Appointment appointment, User user) 
        {
            return new Message(new string[] 
            { user.Email }, 
            "Appointment scheduled", 
            $"Dear {user.Name} {user.Surname},\nWe are pleased to inform you that your appointment has been successfully scheduled.\n\nTime: {appointment.TimeSlot.Start}\nPatient: {appointment.Pet.Name}");
        }
    }
}
