using Backend;

var infix = "4*5/(4+6)";
Console.WriteLine($"Infix = {infix}, Result = {ExpressionEvaluator.Evalute(infix):N5}"); // 2

var infix2 = "4*(5+6-(8/2^3)-7)-1";
Console.WriteLine($"Infix = {infix2}, Result = {ExpressionEvaluator.Evalute(infix2):N5}"); // 11

var infix3 = "4*7^(1/3)*7*((1+9)/3*7^4)";
Console.WriteLine($"Infix = {infix3}, Result = {ExpressionEvaluator.Evalute(infix3):N5}"); // 428,675.12518474100 

var infix4 = "144^(1/2)";
Console.WriteLine($"Infix = {infix4}, Result = {ExpressionEvaluator.Evalute(infix4):N5}"); // 12

// Pruebas para decimales

var infix5 = "12.5*2";
Console.WriteLine($"Infix = {infix5}, Result = {ExpressionEvaluator.Evalute(infix5):N5}"); // 25

var infix6 = "10.25+5.75";
Console.WriteLine($"Infix = {infix6}, Result = {ExpressionEvaluator.Evalute(infix6):N5}"); // 16

var infix7 = "100.5/2";
Console.WriteLine($"Infix = {infix7}, Result = {ExpressionEvaluator.Evalute(infix7):N5}"); // 50.25

var infix8 = "144.0^(1/2)";
Console.WriteLine($"Infix = {infix8}, Result = {ExpressionEvaluator.Evalute(infix8):N5}"); // 12

// pruebas conbinadas

var infix9 = "10.8*(2+5.2)";
Console.WriteLine($"Infix = {infix9}, Result = {ExpressionEvaluator.Evalute(infix9):N5}"); // 77.76

var infix10 = "98.5/(3.1+0.5)";
Console.WriteLine($"Infix = {infix10}, Result = {ExpressionEvaluator.Evalute(infix10):N5}"); // 27.3611

var infix11 = "1.2^2";
Console.WriteLine($"Infix = {infix11}, Result = {ExpressionEvaluator.Evalute(infix11):N5}"); // 1.44

var infix12 = "15.5+2.5*3.2";
Console.WriteLine($"Infix = {infix12}, Result = {ExpressionEvaluator.Evalute(infix12):N5}"); // 23.50