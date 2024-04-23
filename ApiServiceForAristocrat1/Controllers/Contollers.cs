using ApiServiceForAristocrat1.DataBase;
using Microsoft.AspNetCore.Mvc;
using static ApiServiceForAristocrat1.Models.Model;

namespace ApiServiceForAristocrat1.Controllers
{
    #region Авторизация и регистрация Пользователя
    [Route("[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthUserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("Register")]
        public IActionResult RegisterUser([FromBody]User user)
        {
            try
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    return BadRequest("Пользователь с такой почтой уже зарегистрирован.");
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok("Пользователь зарегистрирован");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при регистрации пользователя: {ex.Message}"); ;
            }
        }
        [HttpPost("Login")]
        public IActionResult LoginUser([FromBody]User user)
        {
            try
            {
                var existingUser = _context.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                if (existingUser == null) { return Unauthorized("Неверные учетные данные"); }
                return Ok("Авторизация успешна");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при авторизации пользователя: {ex.Message}");
            }
        }
    }
    #endregion
    #region Авторизация и регистрация Администратора
    [Route("[controller]")]
    [ApiController]
    public class AuthAdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public IActionResult RegisterAdmin([FromBody] Admin admin)
        {
            try
            {
                var existingAdmin = _context.Admins.FirstOrDefault(u => u.Email == admin.Email);
                if (existingAdmin != null)
                {
                    return BadRequest("Администратор с такой почтой уже зарегистрирован.");
                }
                _context.Admins.Add(admin);
                _context.SaveChanges();
                return Ok("Администратор зарегистрирован");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при регистрации Администратора: {ex.Message}"); ;
            }
        }
        [HttpPost("Login")]
        public IActionResult LoginAdmin([FromBody] Admin admin)
        {
            try
            {
                var existingAdmin = _context.Admins.SingleOrDefault(u => u.Email == admin.Email && u.Password ==  admin.Password);
                if (existingAdmin == null) { return Unauthorized("Неверные учетные данные"); }
                return Ok(existingAdmin.FIO);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при авторизации Администратора: {ex.Message}");
            }
        }
    }
    #endregion
    #region Получение, редактирование и удаление продукта
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }
        
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok("Продукт добавлен");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при добавлении продукта: {ex.Message}");
            }
        }
        
        [HttpPut("UpdateProduct/{id}")]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Неверный запрос.");
            }
            var existingProduct = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return NotFound("Продукт не найден.");
            }
            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            _context.SaveChanges();

            return Ok("Продукт обновлен");
        }

        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound("Продукт не найден");

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Продукт успешно удален");
        }
    }

    #endregion
}
