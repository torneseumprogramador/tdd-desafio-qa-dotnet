using System.Text.RegularExpressions;

namespace tdd_desafio_qa_dotnet.Servicos;

public static class Validar
{
    public static bool ValidarCPF(string? cpf)
    {
        if (cpf == null) return false;
        cpf = Regex.Replace(cpf, "\\D+", ""); // Remove todos os caracteres que não são números

        // Verifica se o CPF tem 11 caracteres
        if (cpf.Length != 11)
        {
            return false;
        }

        // Verifica se todos os dígitos são iguais
        bool todosDigitosIguais = true;
        for (int i = 1; i < cpf.Length; i++)
        {
            if (cpf[i] != cpf[0])
            {
                todosDigitosIguais = false;
                break;
            }
        }
        if (todosDigitosIguais)
        {
            return false;
        }

        // Verifica se o primeiro dígito verificador é válido
        int soma = 0;
        for (int i = 0; i < 9; i++)
        {
            soma += (10 - i) * (cpf[i] - '0');
        }
        int digitoVerificador = 11 - (soma % 11);
        if (digitoVerificador >= 10)
        {
            digitoVerificador = 0;
        }
        if (digitoVerificador != (cpf[9] - '0'))
        {
            return false;
        }

        // Verifica se o segundo dígito verificador é válido
        soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += (11 - i) * (cpf[i] - '0');
        }
        digitoVerificador = 11 - (soma % 11);
        if (digitoVerificador >= 10)
        {
            digitoVerificador = 0;
        }
        if (digitoVerificador != (cpf[10] - '0'))
        {
            return false;
        }
        return true;
    }
}