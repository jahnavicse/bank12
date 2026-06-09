
using BankApi.Data;
using BankApi.DTOs;
using BankApi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/transactions
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new
                    {
                        message = "Invalid token"
                    });
                }

                var transactions = await _context.Transactions
                    .Where(t => t.UserId == userId)
                    .OrderByDescending(t => t.Date)
                    .ToListAsync();

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        // GET: api/transactions/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var transaction = await _context.Transactions
                    .FirstOrDefaultAsync(t =>
                        t.Id == id &&
                        t.UserId == userId);

                if (transaction == null)
                {
                    return NotFound(new
                    {
                        message = "Transaction not found"
                    });
                }

                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        // POST: api/transactions
        [HttpPost]
        public async Task<IActionResult> AddTransaction(
            [FromBody] TransactionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new
                    {
                        message = "Invalid token"
                    });
                }

                var transaction = new Transaction
                {
                    Amount = dto.Amount,
                    Type = dto.Type,
                    Category = dto.Category,
                    Description = dto.Description,
                    Date = DateTime.UtcNow,
                    UserId = userId
                };

                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetTransactionById),
                    new { id = transaction.Id },
                    transaction
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/transactions/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(
            int id,
            [FromBody] TransactionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var transaction = await _context.Transactions
                    .FirstOrDefaultAsync(t =>
                        t.Id == id &&
                        t.UserId == userId);

                if (transaction == null)
                {
                    return NotFound(new
                    {
                        message = "Transaction not found"
                    });
                }

                transaction.Amount = dto.Amount;
                transaction.Type = dto.Type;
                transaction.Category = dto.Category;
                transaction.Description = dto.Description;

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Transaction updated successfully",
                    data = transaction
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/transactions/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var transaction = await _context.Transactions
                    .FirstOrDefaultAsync(t =>
                        t.Id == id &&
                        t.UserId == userId);

                if (transaction == null)
                {
                    return NotFound(new
                    {
                        message = "Transaction not found"
                    });
                }

                _context.Transactions.Remove(transaction);

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Transaction deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }
    }
}

