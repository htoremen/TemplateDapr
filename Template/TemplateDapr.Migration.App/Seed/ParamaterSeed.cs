using Domain.Entities;
using Infrastructure.Persistence;

namespace Migration.App.Seed
{
    public static class ParamaterSeed
    {
        public static async Task SeedFormParametersAsync(ApplicationDbContext context)
        {
            if (context == null)
                return;

            if (!context.ParameterTypes.Any())
            {
                var parameterTypeId = "4d4b3802-2128-44c8-ad98-47fe3000c100";
                var parameterType = context.ParameterTypes.Add(new ParameterType { ParameterTypeId = parameterTypeId, Name = "Email İzni" });


                parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-2128-44c8-ad98-47fe3000c101", ParameterTypeId = parameterTypeId, Name = "Randevu Oluşturma Bildirimi", IsActive = true });
                parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-2128-44c8-ad98-47fe3000c102", ParameterTypeId = parameterTypeId, Name = "Randevu Hatırlatma Bildirimi", IsActive = true });
                parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-2128-44c8-ad98-47fe3000c103", ParameterTypeId = parameterTypeId, Name = "Randevu Tarihi Güncelleme Bildirimi", IsActive = true });
                parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-2128-44c8-ad98-47fe3000c104", ParameterTypeId = parameterTypeId, Name = "Randevu Durumu Güncelleme Bildirimi", IsActive = true });
                parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-2128-44c8-ad98-47fe3000c105", ParameterTypeId = parameterTypeId, Name = "Günlük Randevu Hatırlatma Bildirimi", IsActive = true });
                parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-2128-44c8-ad98-47fe3000c106", ParameterTypeId = parameterTypeId, Name = "Randevu Yorumu Bildirimi", IsActive = true });

                await context.SaveChangesAsync();
            }
        }
    }
}