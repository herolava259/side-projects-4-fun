using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CalculatorController : ControllerBase
    {
        private readonly DataContext _context;
        public CalculatorController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        [ActionName("histories")]
        public async Task<ActionResult<IEnumerable<CalculatorHistory>>> GetCalculationHistories()
        {
            return await _context.CalculatorHistories.OrderByDescending(h => h.Id).ToListAsync();
        }


        [HttpPost]
        [ActionName("addcalculation")]
        public async Task<ActionResult<CalculatorHistory>> AddCalculation([FromBody] CalculationDTO calcDTO)
        {
            int firstOperand = calcDTO.FirstOperand;
            int lastOperand = calcDTO.LastOperand;

            string algOperator = calcDTO.AlgOperator;

            float result;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            switch (algOperator)
            {
                case "+":
                    {
                        result = firstOperand + lastOperand;
                        break;
                    }

                case "-":
                    {
                        result = firstOperand - lastOperand;
                        break;
                    }

                case "*":
                    {
                        result = lastOperand * firstOperand;
                        break;
                    }

                case "/":
                    {
                        result = (float)firstOperand / (float)lastOperand;
                        break;
                    }

                default:
                    {
                        Console.WriteLine($"algebaic Operator{algOperator}");
                        ModelState.AddModelError("ErrorMessages", "Inputs are invalid");
                        return BadRequest(ModelState);

                    }
            }
            var obj = new CalculatorHistory
            {
                FirstOperand = firstOperand,
                LastOperand = lastOperand,
                AlgOperator = algOperator,
                Result = result,
            };

            _context.CalculatorHistories.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        [HttpDelete("{id:int}")]
        [ActionName("delete")]
        public async Task<ActionResult<bool>> DeleteHistory(int id = 0)
        {
            if (id == 0)
            {
                var objList = await _context.CalculatorHistories.ToListAsync();

                _context.CalculatorHistories.RemoveRange(objList);

                await _context.SaveChangesAsync();

                int afterCount = await _context.CalculatorHistories.CountAsync();

                if (afterCount > 0)
                {
                    return false;
                }
            }else{
                var obj = await _context.CalculatorHistories.Where(h => h.Id == id).FirstOrDefaultAsync();
                if(obj == null)
                {
                    return false;
                }else{
                    _context.CalculatorHistories.Remove(obj);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            return true;
        }

    }
}