﻿using System.Numerics;
using WebAppWithCRUD.dto;
using WebAppWithCRUD.dto.Request;
using WebAppWithCRUD.dto.Response;
using WebAppWithCRUD.Models;
using WebAppWithCRUD.Repositories.Interfaces;
using WebAppWithCRUD.Services.Interfaces;

namespace WebAppWithCRUD.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(
            IClientRepository repository,
            ILogger<ClientService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Delete the client by id.
        /// </summary>
        /// <param name="id">client id.</param>
        /// <returns>
        /// The ServiceResponse with the bool if the request
        /// was succesfull or with the corresponding error otherwise.
        /// </returns>
        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                if (!await _repository.IsClientExists(id))
                {
                    var error = new ValidationError()
                    {
                        FieldName = nameof(id),
                        ErrorDetail = "Client not found",
                    };
                    return new ServiceResponse<bool>(new List<ValidationError>() { error });
                }

                await _repository.DeleteAsync(id);

                return new ServiceResponse<bool>(true);
            }
            catch (Exception ex)
            {
                var location = "ClientService.DeleteAsync";
                this._logger.LogError(ex, $"{location}: {ex.Message}");
                var error = new InternalError { Location = location, Message = ex.Message };
                return new ServiceResponse<bool>(error);
            }
        }

        /// <summary>
        /// Gets all clients from the repository.
        /// </summary>
        /// <returns>
        /// The ServiceResponse with the list of ClientItemResponse if the request
        /// was succesfull or with the corresponding error otherwise.
        /// </returns>
        public async Task<ServiceResponse<IReadOnlyList<ClientItemResponse>>> GetAllAsync()
        {
            try
            {
                var response = new List<ClientItemResponse>();
                var list = await _repository.GetAllAsync();
                if (list == null || list.Count == 0) 
                    return new ServiceResponse<IReadOnlyList<ClientItemResponse>>(response);

                foreach (var client in list)
                {
                    var cellphone = client.PhoneExtension.Length == 1 ?
                        client.PhoneExtension + client.PhoneNumber :
                        "+" + client.PhoneExtension + client.PhoneNumber;

                    response.Add(new ClientItemResponse
                    {
                        Id = client.Id,
                        Name = client.Name,
                        Email = client.Email,
                        Cellphone = client.PhoneExtension.Length == 1 ?
                            client.PhoneExtension + client.PhoneNumber :
                            "+" + client.PhoneExtension + client.PhoneNumber,
                        EmailStatus = client.EmailStatus,
                        SmsStatus = client.SmsStatus,
                    });
                }

                return new ServiceResponse<IReadOnlyList<ClientItemResponse>>(response);
            }
            catch (Exception ex)
            {
                var location = "ClientService.GetAllAsync";
                this._logger.LogError(ex, $"{location}: {ex.Message}");
                var error = new InternalError { Location = location, Message = ex.Message };
                return new ServiceResponse<IReadOnlyList<ClientItemResponse>>(error);
            }
        }

        /// <summary>
        /// Gets the client by id.
        /// </summary>
        /// <param name="id">client id.</param>
        /// <returns>
        /// The ServiceResponse with the ClientItemResponse if the request
        /// was succesfull or with the corresponding error otherwise.
        /// </returns>
        public async Task<ServiceResponse<ClientItemResponse>> GetByIdAsync(int id)
        {
            try
            {
                var client = await _repository.GetByIdAsync(id);
                if (client == null)
                {
                    var error = new ValidationError()
                    {
                        FieldName = nameof(id),
                        ErrorDetail = "Client not found",
                    };
                    return new ServiceResponse<ClientItemResponse>(new List<ValidationError>() { error });
                }

                var response = new ClientItemResponse
                {
                    Name = client.Name,
                    Email = client.Email,
                    Cellphone = '+' + client.PhoneExtension + client.PhoneExtension,
                    EmailStatus = client.EmailStatus,
                    SmsStatus = client.SmsStatus,
                };

                return new ServiceResponse<ClientItemResponse>(response);
            }
            catch (Exception ex)
            {
                var location = "ClientService.GetByIdAsync";
                this._logger.LogError(ex, $"{location}: {ex.Message}");
                var error = new InternalError { Location = location, Message = ex.Message };
                return new ServiceResponse<ClientItemResponse>(error);
            }
        }

        /// <summary>
        /// Insert the new client.
        /// </summary>
        /// <param name="request">InsertClientRequest.</param>
        /// <returns>
        /// The ServiceResponse with the client id if the request
        /// was succesfull or with the corresponding error otherwise.
        /// </returns>
        public async Task<ServiceResponse<int>> InsertBySPAsync(InsertClientRequest request)
        {
            try
            {
                var errorsList = new List<ValidationError>();
                var phone = DividePhoneNumber(request.Cellphone);

                var model = new Client()
                {
                    Email = request.Email,
                    Name = request.Name,
                    PhoneExtension = phone.ext,
                    PhoneNumber = phone.number,
                };

                var result = await _repository.InsertBySPAsync(model);

                if (result.error.Length > 0)
                {
                    errorsList.Add(new ValidationError()
                    {
                        ErrorDetail = result.error,
                    });
                }

                if (errorsList.Count > 0)
                {
                    return new ServiceResponse<int>(errorsList);
                }

                return new ServiceResponse<int>(result.id);
            }
            catch (Exception ex)
            {
                var location = "ClientService.InsertAsync";
                this._logger.LogError(ex, $"{location}: {ex.Message}");
                var error = new InternalError { Location = location, Message = ex.Message };
                return new ServiceResponse<int>(error);
            }
        }

        /// <summary>
        /// Update the client.
        /// </summary>
        /// <param name="request">UpdateClientRequest.</param>
        /// <returns>
        /// The ServiceResponse with the bool if the request
        /// was succesfull or with the corresponding error otherwise.
        /// </returns>
        public async Task<ServiceResponse<bool>> UpdateBySPAsync(int id, UpdateClientRequest request)
        {
            try
            {
                var errorsList = new List<ValidationError>();
                var client = await _repository.GetByIdAsync(id);
                if (client == null)
                {
                    errorsList.Add(new ValidationError()
                    {
                        FieldName = nameof(id),
                        ErrorDetail = "Client not found",
                    });

                    return new ServiceResponse<bool>(errorsList);
                }

                var phone = DividePhoneNumber(request.Cellphone);

                client.Name = request.Name;
                client.Email = request.Email;
                client.PhoneExtension = phone.ext;
                client.PhoneNumber = phone.number;
                client.EmailStatus = request.EmailStatus;
                client.SmsStatus = request.SmsStatus;

                var result = await _repository.UpdateBySPAsync(client);
                if (string.IsNullOrEmpty(result))
                    return new ServiceResponse<bool>(true);
                else
                {
                    errorsList.Add(new ValidationError()
                    {
                        ErrorDetail = result,
                    });

                    return new ServiceResponse<bool>(errorsList);
                }
            }
            catch (Exception ex)
            {
                var location = "ClientService.UpdateBySPAsync";
                this._logger.LogError(ex, $"{location}: {ex.Message}");
                var error = new InternalError { Location = location, Message = ex.Message };
                return new ServiceResponse<bool>(error);
            }
            throw new NotImplementedException();
        }


        /// <summary>
        /// Divide the phone number on extension and number.
        /// </summary>
        /// <param name="phoneNumber">client phone number.</param>
        /// <returns>
        /// (extension string, number string)
        /// </returns>
        private (string ext, string number) DividePhoneNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber.Contains('+') ? phoneNumber.Remove(0, 1) : phoneNumber;

            var ext = phoneNumber.StartsWith("05") ? "0" : "972";

            var number = ext.Length == 1 ? phoneNumber.Remove(0, 1) : phoneNumber.Remove(0, 3);

            return (ext,  number);
        }
    }
}
