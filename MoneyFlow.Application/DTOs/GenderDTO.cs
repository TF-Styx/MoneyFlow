﻿using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Application.DTOs
{
    public class GenderDTO
    {
        private GenderDTO(int idGender, string genderName)
        {
            IdGender = idGender;
            GenderName = genderName;
        }

        public int IdGender { get; private set; }
        public string? GenderName { get; private set; }

        public static (GenderDTO GenderDTO, string Message) Create(int idGender, string genderName)
        {
            var message = string.Empty;

            if (genderName.Length > IntConstants.MAX_GENDER_NAME_LENGHT)
            {
                return (null, "Превышена длина слова в «20» символов");
            }

            var gender = new GenderDTO(idGender, genderName);

            return (gender, message);
        }

        public GenderDTO SetProperty(int idGender, string genderName)
        {
            IdGender = idGender;
            GenderName = genderName;

            return this;
        }
    }
}
