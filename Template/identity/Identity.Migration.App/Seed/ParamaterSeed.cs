using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence;

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
                await EmailIzinAsync(context);
                await UyelikAsync(context);
            }
        }


        private static async Task UyelikAsync(ApplicationDbContext context)
        {
            var id = "c4140313-9344-4170-9b31-3bc3ce2bc200";
            var type = context.ParameterTypes.Add(new ParameterType { ParameterTypeId = id, Name = "Üyelik Aşamaları" });

            type.Entity.Parameters.Add(new Parameter
            {
                ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc201",
                ParameterTypeId = id,
                ParameterName = "Hangi dili kullanmak istiyorsunuz?",
                OrderId = 1,
                IsActive = true,
                ParameterStages = new List<ParameterStage>()
            {
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2bc2011", Name = "Türkçe", OrderId = 1, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2bc2012", Name = "English", OrderId = 2, IsActive = true}
            }
            });

            type.Entity.Parameters.Add(new Parameter
            {
                ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc202",
                ParameterTypeId = id,
                ParameterName = "Ne öğrenmek istersin?",
                OrderId = 2,
                IsActive = true,
                ParameterStages = new List<ParameterStage>()
            {
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2021", Name = "İngilizce", OrderId = 1, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2022", Name = "Tarih", OrderId = 2, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2023", Name = "Coğrafya", OrderId = 3, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2024", Name = "Biyoloji", OrderId = 4, IsActive = true},
            }
            });

            type.Entity.Parameters.Add(new Parameter
            {
                ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc203",
                ParameterTypeId = id,
                ParameterName = "Nereden buldun",
                OrderId = 3,
                IsActive = true,
                ParameterStages = new List<ParameterStage>()
            {
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2031", Name = "Google", OrderId = 1, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2032", Name = "Facebook", OrderId = 2, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2033", Name = "Tiktok", OrderId = 3, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2034", Name = "Youtube", OrderId = 4, IsActive = true},
            }
            });

            type.Entity.Parameters.Add(new Parameter
            {
                ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc204",
                ParameterTypeId = id,
                ParameterName = "Ne kadar biliyorsun",
                OrderId = 4,
                IsActive = true,
                ParameterStages = new List<ParameterStage>()
            {
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2041", Name = "Başlangıç", OrderId = 1, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2042", Name = "Biraz", OrderId = 2, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2043", Name = "İyi", OrderId = 3, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2044", Name = "Çok iyi", OrderId = 4, IsActive = true},
            }
            });

            type.Entity.Parameters.Add(new Parameter
            {
                ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc205",
                ParameterTypeId = id,
                ParameterName = "Ne için öğreniyorsun",
                OrderId = 5,
                IsActive = true,
                ParameterStages = new List<ParameterStage>()
            {
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2051", Name ="Eğlence", OrderId = 1, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2052", Name ="Genel Kültür", OrderId = 2, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2053", Name ="Eğitim", OrderId = 3, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2054", Name ="Gezi ve Seyahat", OrderId = 4, IsActive = true},
            }
            });

            type.Entity.Parameters.Add(new Parameter
            {
                ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc206",
                ParameterTypeId = id,
                ParameterName = "Günlük çalışma hedefiniz nedir?",
                OrderId = 6,
                IsActive = true,
                ParameterStages = new List<ParameterStage>()
            {
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2061", Name = "5 dk / Günlük", Description = "Rahat", OrderId = 1, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2062", Name = "10 dk / Günlük", Description = "Normal", OrderId = 2, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2063", Name = "15 dk / Günlük", Description = "Ciddi", OrderId = 3, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2064", Name = "30 dk / Günlük", Description = "Harika", OrderId = 4, IsActive = true},
                new ParameterStage{ ParameterStageId = "c4140313-9344-4170-9b31-3bc3ce2b2065", Name = "60 dk / Günlük", Description = "Mükemmel", OrderId = 5, IsActive = true},
            }
            });

            type.Entity.Parameters.Add(new Parameter { ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc207", ParameterTypeId = id, ParameterName = "Muhteşem", Description = "Şimdi profilinizi oluşturun", OrderId = 7, IsActive = true });
            type.Entity.Parameters.Add(new Parameter { ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc208", ParameterTypeId = id, ParameterName = "Adınız", OrderId = 8, IsActive = true });
            type.Entity.Parameters.Add(new Parameter { ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc209", ParameterTypeId = id, ParameterName = "Kaç yaşındasınız?", OrderId = 9, IsActive = true });
            type.Entity.Parameters.Add(new Parameter { ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc210", ParameterTypeId = id, ParameterName = "E-Posta adresiniz?", OrderId = 10, IsActive = true });
            type.Entity.Parameters.Add(new Parameter { ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc211", ParameterTypeId = id, ParameterName = "Şifreniz?", OrderId = 11, IsActive = true });
            type.Entity.Parameters.Add(new Parameter { ParameterId = "c4140313-9344-4170-9b31-3bc3ce2bc212", ParameterTypeId = id, ParameterName = "Yaşasın?", Description = "Hoş geldiniz", OrderId = 12, IsActive = true });

            await context.SaveChangesAsync();
        }

        private static async Task EmailIzinAsync(ApplicationDbContext context)
        {
            var parameterTypeId = "4d4b3802-3130-44c8-ad98-47fe3000c100";
            var parameterType = context.ParameterTypes.Add(new ParameterType { ParameterTypeId = parameterTypeId, Name = "Email İzni" });

            parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-3130-44c8-ad98-47fe3000c101", ParameterTypeId = parameterTypeId, ParameterName = "Randevu Oluşturma Bildirimi", IsActive = true });
            parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-3130-44c8-ad98-47fe3000c102", ParameterTypeId = parameterTypeId, ParameterName = "Randevu Hatırlatma Bildirimi", IsActive = true });
            parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-3130-44c8-ad98-47fe3000c103", ParameterTypeId = parameterTypeId, ParameterName = "Randevu Tarihi Güncelleme Bildirimi", IsActive = true });
            parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-3130-44c8-ad98-47fe3000c104", ParameterTypeId = parameterTypeId, ParameterName = "Randevu Durumu Güncelleme Bildirimi", IsActive = true });
            parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-3130-44c8-ad98-47fe3000c105", ParameterTypeId = parameterTypeId, ParameterName = "Günlük Randevu Hatırlatma Bildirimi", IsActive = true });
            parameterType.Entity.Parameters.Add(new Parameter { ParameterId = "4d4b3802-3130-44c8-ad98-47fe3000c106", ParameterTypeId = parameterTypeId, ParameterName = "Randevu Yorumu Bildirimi", IsActive = true });

            await context.SaveChangesAsync();
        }
    }
}