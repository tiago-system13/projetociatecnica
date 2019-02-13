(function ($) {
    'use strict';
      
    $("#cpf").mask("999.999.999-99")
    $("#cnpj").mask("99.999.999/9999-99");
    $('#div_endereco').hide();
    function validarInteiros() {

        if ($("#cnpj").val() !== "") {
            var valor = $("#cnpj").val().match(/\d|,/g);
            if (!valor) {
                $("#cnpj").val('');
            }
        }
       
        if ($("#cpf").val() !== "") {
            var valor = $("#cpf").val().match(/\d|,/g);
            if (!valor) {
                $("#cpf").val('');
            }
        }
        if ($("#cep").val() !== "") {
            var valor = $("#cep").val().match(/\d|,/g);
            if (!valor) {
                $("#cep").val('');
            }
        }       
    }
    $("#cnpj").on('keyup',function () {
        validarInteiros();
    })
    $("#cep").on('keyup',function () {
        validarInteiros();
    })
    $("#cpf").on('keyup',function () {
        validarInteiros();
       
           
    })
    $('#cpf').on('blur', function () {
        var cpf = $('#cpf').val().replace('.', '').replace('.', '').replace('.', '').replace('-', '');
        if (!validarCPF(cpf)) {
            $('#div_cpf').find('span').html('');
            $('#div_cpf').append(
                   '<span class="form-group has-error field-validation-error" data-valmsg-for="data"  data-val-replace="true">' +
                       '<span style="color:red" class="" id="data-error">CPF inválido!.</span>' +
                       '</span>');

            $('#cpf').focus();
        } else {
            $('#div_cpf').find('span').html('');
        }
    })

    $('#cnpj').on('blur', function () {
        var cnpj = $('#cnpj').val().replace('.', '').replace('.', '').replace('/', '').replace('-', '');
        if (!validandocnpj(cnpj)) {
            $('#div_cnpj').find('span').html('');
            $('#div_cnpj').append(
                   '<span class="form-group has-error field-validation-error" data-valmsg-for="data"  data-val-replace="true">' +
                       '<span style="color:red" class="" id="data-error">CNPJ inválido!.</span>' +
                       '</span>');

            $('#cnpj').focus();
        } else {
            $('#div_cnpj').find('span').html('');
        }
    })
        
    $('#div_field_pj').hide();
    $('#div_field_pf').hide();

    if ($('#pessoa').val() > 0) {
        $('#div_endereco').show();
    }

    if ($('#tipoDePessoa').val() !== '' && $('#tipoDePessoa').val() === 'PJ') {
        $('#div_field_pj').show();
        $('#div_field_pf').hide();
    } else if ($('#tipoDePessoa').val() !== '' && $('#tipoDePessoa').val() === 'PF') {
        $('#div_field_pj').hide();
        $('#div_field_pf').show();
    } else {
        $('#div_field_pj').hide();
        $('#div_field_pf').hide();
        $('#div_endereco').hide();
    }


    $('#tipoDePessoa').change(function () {
        $('#div_endereco').show();
        if ($('#tipoDePessoa').val() !== '' && $('#tipoDePessoa').val() === 'PJ') {            
            $('#div_field_pj').show();
            $('#div_field_pf').hide();
        } else if ($('#tipoDePessoa').val() !== '' && $('#tipoDePessoa').val() === 'PF') {
            $('#div_field_pj').hide();
            $('#div_field_pf').show();
        } else {
            $('#div_field_pj').hide();
            $('#div_field_pf').hide();
            $('#div_endereco').hide();
        }
    });

    if ($('#pessoa').val() !== undefined) {
        if ($('#tipoDePessoa').val() !== '' && $('#tipoDePessoa').val() === 'PJ') {
            $('#div_field_pj').show();
            $('#div_field_pf').hide();
        } else if ($('#tipoDePessoa').val() !== '' && $('#tipoDePessoa').val() === 'PF') {
            $('#div_field_pj').hide();
            $('#div_field_pf').show();
        }
        $('#div_tipo_pessoa').hide();
    }

    $('#datetimepicker2').datetimepicker({
        format: 'DD/MM/YYYY',
        locale: 'pt-br'
    }).on("dp.change", function () {
        if ($('#dataNascimento').val() != '') {
            $('#div_data').find('span').html('');
        }
    });

    $('#btnSalvar').click(function () {


        if ($('#dataNascimento').val() === "" && $('#tipoDePessoa').val() === 'PF') {
            $('#div_data').find('span').html('');
            $('#div_data').append(
                   '<span class="form-group has-error field-validation-error" data-valmsg-for="data"  data-val-replace="true">' +
                       '<span style="color:red" class="" id="data-error">Informe a data de nascimento.</span>' +
                       '</span>');

            $('#dataNascimento').focus();
            return false;
        } else {
            $('#div_data').find('span').html('');
        }
       
            var tipoPessoa = $('#tipoDePessoa').val();
            if (tipoPessoa === 'PF') {
                var cpf = $('#cpf').val().replace('.', '').replace('.', '').replace('.', '').replace('-', '');
                var nome = $('#nome').val();
                var sobrenome = $('#sobrenome').val();
                $('#NumeroDocumento').val(cpf);
                $('#Nome').val(nome);
                $('#SobreNome').val(sobrenome);
            } else if (tipoPessoa === 'PJ') {
                var cnpj = $('#cnpj').val().replace('.', '').replace('.', '').replace('/', '').replace('-', '');
                var nome = $('#fantasia').val();
                var sobrenome = $('#razaoSocial').val();
            $('#NumeroDocumento').val(cnpj);
            $('#Nome').val(nome);
            $('#SobreNome').val(sobrenome);
            }
       
    });

    function validarCPF (cpfInformado) {
        if (cpfInformado != '' && cpfInformado != undefined) {
            var cpf = cpfInformado;
            var numeros, digitos, soma, i, resultado, digitos_iguais;
            digitos_iguais = 1;
            if (cpf.length < 11)
                return false;
            for (i = 0; i < cpf.length - 1; i++)
                if (cpf.charAt(i) != cpf.charAt(i + 1)) {
                    digitos_iguais = 0;
                    break;
                }
            if (!digitos_iguais) {
                numeros = cpf.substring(0, 9);
                digitos = cpf.substring(9);
                soma = 0;
                for (i = 10; i > 1; i--)
                    soma += numeros.charAt(10 - i) * i;
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(0))
                    return false;
                numeros = cpf.substring(0, 10);
                soma = 0;
                for (i = 11; i > 1; i--)
                    soma += numeros.charAt(11 - i) * i;
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(1))
                    return false;
                return true;
            }
            else
                return false;
        } else {
            return true
        }
    };

   function validandocnpj(cnpj) {
        if (cnpj != '' && cnpj != undefined) {
            var input_cnpj = cnpj;
            if (input_cnpj) {
                var input = input_cnpj.toString();
                var pesos_A = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
                var pesos_B = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
                var sum = 0;
                var x1 = 0;
                var x2 = 0;
                for (var i = 0; i < 12; i++) {
                    sum = sum + input[i] * pesos_A[i];
                }
                //calcula digito 1
                var mod = sum % 11;
                if (mod >= 2) {
                    x1 = 11 - mod;
                }
                //calcula digito 2
                sum = 0;
                for (var i = 0; i < 13; i++) {
                    sum = sum + input[i] * pesos_B[i];
                }
                var mod = sum % 11;
                if (mod >= 2) {
                    x2 = 11 - mod;
                }

                //test digitos
                if (x1 == input[12] && x2 == input[13]) {
                    return true;
                } else {                    
                    return false;
                }
            } else {                
                return true;
            }
        } else {            
            return true;
        }
    };


})($ || jquery);