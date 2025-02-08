using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Shared
{
    public class UseCaseResponse<T>
    {
        public UseCaseResponseKind Status { get; private set; }
        public T? Result { get; private set; }
        public long? TotalRows { get; private set; }
        public string? ResultId { get; private set; }
        public string? ErrorMessage { get; private set; }
        public IEnumerable<ErrorMessage>? Errors { get; private set; }

        protected UseCaseResponse(UseCaseResponseKind status, T result, string? errorMessage = null, string? resultId = null, int? totalRows = null)
        {
            Status = status;
            Result = result;
            ResultId = resultId;
            TotalRows = totalRows;
            ErrorMessage = errorMessage;
        }

        protected UseCaseResponse(UseCaseResponseKind status, string errorMessage = null!, IEnumerable<ErrorMessage> errors = null!)
        {
            ErrorMessage = errorMessage;
            Status = status;
            Errors = errors;
        }

        public static UseCaseResponse<T> Persisted(T result, string resultId) => new(UseCaseResponseKind.DataPersisted, result, resultId);
        public static UseCaseResponse<T> Success(T result, int? totalRows = null) => new(UseCaseResponseKind.Success, result, totalRows: totalRows);
        public static UseCaseResponse<T> Unavailable(T result) => new(UseCaseResponseKind.Unavailable, result);
        public static UseCaseResponse<T> NotFound(IEnumerable<ErrorMessage> errors) => new(UseCaseResponseKind.NotFound, "Data not found", errors);
        public static UseCaseResponse<T> NotFound(T result) => new(UseCaseResponseKind.NotFound, result, "Data not found");
        public static UseCaseResponse<T> BadRequest(IEnumerable<ErrorMessage> errors) => new(UseCaseResponseKind.BadRequest, "Bad Request", errors);
        public static UseCaseResponse<T> BadGateway(IEnumerable<ErrorMessage> errors) => new(UseCaseResponseKind.BadGateway, "Bad Gateway", errors);
        public static UseCaseResponse<T> BadRequest(string errorMessage, IEnumerable<ErrorMessage> errors) => new(UseCaseResponseKind.BadRequest, errorMessage, errors);
        public static UseCaseResponse<T> BadGateway(string errorMessage, IEnumerable<ErrorMessage> errors) => new(UseCaseResponseKind.BadGateway, errorMessage, errors);
        public static UseCaseResponse<T> InternalServerError(IEnumerable<ErrorMessage> errors) => new(UseCaseResponseKind.InternalServerError, "Internal Server Error", errors);
        public static UseCaseResponse<T> InternalServerError(T result) => new(UseCaseResponseKind.InternalServerError, result, "Internal Server Error");


        public UseCaseResponse<T> SetSuccess(T result)
        {
            Result = result;
            return SetStatus(UseCaseResponseKind.Success);
        }

        public UseCaseResponse<T> SetResult(T result)
        {
            Result = result;
            return SetResult(null, UseCaseResponseKind.OK);
        }

        public UseCaseResponse<T> SetError(string errorMessage, UseCaseResponseKind status, IEnumerable<ErrorMessage> errors = null!)
        {
            return SetResult(errorMessage, status, errors);
        }

        public UseCaseResponse<T> SetBadRequest(string errorMessage, IEnumerable<ErrorMessage> errors = null!)
        {
            return SetError(errorMessage, UseCaseResponseKind.BadRequest, errors);
        }

        public UseCaseResponse<T> SetInternalServerError(string errorMessage, IEnumerable<ErrorMessage> errors = null!)
        {
            return SetStatus(UseCaseResponseKind.InternalServerError, errorMessage, errors);
        }

        public UseCaseResponse<T> SetUnavailable(T result)
        {
            Result = result;
            Status = UseCaseResponseKind.Unavailable;
            ErrorMessage = "Service Unavailable";
            return this;
        }

        public UseCaseResponse<T> SetRequestValidationError(string errorMessage, IEnumerable<ErrorMessage> errors = null!)
        {
            return SetStatus(UseCaseResponseKind.RequestValidationError, errorMessage, errors);
        }

        public UseCaseResponse<T> SetNotFound(ErrorMessage error)
        {
            return SetStatus(UseCaseResponseKind.NotFound, "Data not found", new ErrorMessage[] { error });
        }

        public UseCaseResponse<T> SetBadGateway(string errorMessage, IEnumerable<ErrorMessage> errors = null!)
        {
            return SetStatus(UseCaseResponseKind.BadGateway, errorMessage, errors);
        }

        public UseCaseResponse<T> SetDataAccepted()
        {
            return SetStatus(UseCaseResponseKind.DataAccepted);
        }

        public UseCaseResponse<T> SetStatus(UseCaseResponseKind status, string? errorMessage = null, IEnumerable<ErrorMessage> errors = null!)
        {
            Status = status;
            ErrorMessage = errorMessage;
            Errors = errors;

            return this;
        }

        public bool Success()
        {
            return ErrorMessage is null;
        }

        private UseCaseResponse<T> SetResult(string? errorMessage, UseCaseResponseKind status, IEnumerable<ErrorMessage>? errors = null)
        {
            ErrorMessage = errorMessage;
            Status = status;
            Errors = errors;

            return this;
        }
    }
}
