namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class ExpenseMapper
{
    extension(ExpenseDatabaseEntity dbEntity)
    {
        public Expense FromPersistence() => 
            Expense.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Description,
                (ExpenseType)dbEntity.ExpenseTypeId,
                dbEntity.Amount,
                (ExpenseFrequency)dbEntity.Frequency,
                dbEntity.EffectiveFrom,
                dbEntity.EffectiveTo,
                dbEntity.CreatedAt,
                dbEntity.DayOfMonth,
                dbEntity.DayOfWeek,
                dbEntity.SpecificPayableDate,
                dbEntity.DaysWorkedPerWeek,
                dbEntity.CalculateDailyRate,
                dbEntity.Location?.PublicId,
                dbEntity.Employee?.PublicId);
    }

    extension(Expense domainEntity)
    {
        public ExpenseDatabaseEntity ToPersistence() =>
            new()
            {
                PublicId = domainEntity.PublicId,
                Description = domainEntity.Description,
                ExpenseTypeId = (int)domainEntity.Type,
                Amount = domainEntity.Amount,
                Frequency = (int)domainEntity.Frequency,
                EffectiveFrom = domainEntity.EffectiveFrom,
                EffectiveTo = domainEntity.EffectiveTo,
                CreatedAt = domainEntity.CreatedAt,
                DayOfMonth = domainEntity.DayOfMonth,
                DayOfWeek = domainEntity.DayOfWeek,
                SpecificPayableDate = domainEntity.SpecificPayableDate,
                DaysWorkedPerWeek = domainEntity.DaysWorkedPerWeek,
                CalculateDailyRate = domainEntity.CalculateDailyRate,
                //LocationId = domain.LocationId, // TODO: To be set on the object in infrastructure layer 
                //EmployeeId = domain.EmployeeId // TODO: To be set on the object in infrastructure layer 
            };

        public void UpdatePersistence(ExpenseDatabaseEntity dbEntity)
        {
            dbEntity.Description = domainEntity.Description;
            dbEntity.ExpenseTypeId = (int)domainEntity.Type;
            dbEntity.Amount = domainEntity.Amount;
            dbEntity.Frequency = (int)domainEntity.Frequency;
            dbEntity.EffectiveFrom = domainEntity.EffectiveFrom;
            dbEntity.EffectiveTo = domainEntity.EffectiveTo;
            dbEntity.DayOfMonth = domainEntity.DayOfMonth;
            dbEntity.DayOfWeek = domainEntity.DayOfWeek;
            dbEntity.SpecificPayableDate = domainEntity.SpecificPayableDate;
            dbEntity.DaysWorkedPerWeek = domainEntity.DaysWorkedPerWeek;
            dbEntity.CalculateDailyRate = domainEntity.CalculateDailyRate;
            //entity.LocationId = domain.LocationId; // TODO: To be set on the object in infrastructure layer 
            //entity.EmployeeId = domain.EmployeeId; // TODO: To be set on the object in infrastructure layer 
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







