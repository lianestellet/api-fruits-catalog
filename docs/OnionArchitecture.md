# Onion Architecture
These are some alternative names for the Onion Layers.

| Projects         | **Business Logic**   |   **Data Access** | **Entities**  |
|-----------------|------------|------------|------------|
| | Services  | Repositories       | Core|
| | Handlers | Infrastructure | Domain  |
| | Application    |    Data  | CoreEntities|

## Request Flow

1. **API Layer**:
   - **Controller**: The request starts at the `FruitController`. It handles HTTP requests and maps them to the appropriate business logic `FindAllFruits()` at `FruitService.cs`
   - **Example**: User requests `GET /api/fruits`.

2. **Business Logic Layer (Core)**:
   - **Service**: The `FruitService.cs` handles the business rules. It processes the request and performs necessary actions like validation and data transformation.
   - **Example**: `FruitService.cs` calls `FindAllFruits()` from `FruitRepository.cs` to retrieve the list of fruits.

3. **Data Access Layer**:
   - **Repository**: The `FruitRepository.cs` communicates with the database. It retrieves the necessary data and maps it to domain entities.
   - **Example**: `FruitRepository.cs` calls the `FruitDbContext` to fetch data from the database.

4. **Entities Layer**:
   - **Domain Models**: Represents the core business entities like `Fruit` and `FruitType`. These models define the data structure and relationships.
   - **DTO (Data Transfer Object)**: DTOs are simple objects that should not contain any business logic. They are used purely for data transfer.


### Example

1. **API Layer**:
   - **FruitController**:
     ```csharp
     [HttpGet]
     public async Task<ActionResult<IEnumerable<FruitDTO>>> GetAllFruits()
     {
         var fruits = await _fruitService.FindAllFruits();
         return Ok(fruits);
     }
     ```

2. **Business Logic Layer (Core)**:
   - **FruitService**:
     ```csharp
     public class FruitService : IFruitService
     {
         private readonly IFruitRepository _fruitRepository;

         public FruitService(IFruitRepository fruitRepository)
         {
             _fruitRepository = fruitRepository;
         }

         public async Task<IEnumerable<FruitDTO>> FindAllFruits()
         {
             return await _fruitRepository.FindAllFruits();
         }
     }
     ```

3. **Data Access Layer**:
   - **FruitRepository**:
     ```csharp
     public class FruitRepository : IFruitRepository
     {
         private readonly FruitDbContext _context;

         public FruitRepository(FruitDbContext context)
         {
             _context = context;
         }

         public async Task<IEnumerable<FruitDTO>> FindAllFruits()
         {
             return await _context.Fruits
                 .Include(f => f.FruitType)
                 .Select(f => new FruitDTO
                 {
                     Id = f.Id,
                     Name = f.Name,
                     Description = f.Description,
                     FruitType = new FruitTypeDTO
                     {
                         Id = f.FruitType.Id,
                         Name = f.FruitType.Name,
                         Description = f.FruitType.Description
