(function ($) {
    'use strict';

    var Id = '';
    var tipoPessoa = '';
    var PesquisaClientesDatatable = function () {
        $("#datatable-clientes").DataTable({
            destroy: true,
            responsive: true,
            processing: true,
            ajax: {
                url: baseUrl('Pessoa/ListarClientes'),
                dataSrc: 'Lista',
                dataFilter: function (data) {
                    var json = JSON.parse(data);
                    json.recordsTotal = json.TotalDeRegistros;
                    json.recordsFiltered = json.TotalDeRegistros;
                    json.data = json.Lista;
                    return JSON.stringify(json);
                },
                bAutoWidth: true,
                data: function (data) {
                    var documento = '';
                    if ($('#tipodePessoa').val() === 'PF') {
                        documento = $('#documento').val().replace('.', '').replace('.', '').replace('.', '').replace('-', '');
                        
                    } else if ($('#tipodePessoa').val() === 'PJ') {
                        documento = $('#documento').val().replace('.', '').replace('.', '').replace('/', '').replace('-', '');
                       
                    }
                    return $.extend({}, data, {
                        NumeroDocumento: documento,
                        Nome: $('#nome').val(),                        
                        TipoPessoa: $('#tipodePessoa').val(),
                        IndiceDePagina: data.start / data.length + 1,
                        RegistrosPorPagina: data.length,
                        Ordenacao: data.order[0].dir,
                        Coluna: data.columns[data.order[0].column].data
                    });
                }
            },
            initComplete: function () {
                this.api().columns().every(function () {
                    var column = this;
                    var select = $('<select><option value=""></option></select>')
                        .appendTo($(column.footer()).empty())
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex(
                                $(this).val()
                            );

                            column
                                .search(val ? '^' + val + '$' : '', true, false)
                                .draw();
                        });

                    column.data().unique().sort().each(function (d) {
                        select.append('<option value="' + d + '">' + d + '</option>')
                    });
                });
            },

            columnDefs: [{
                targets: -1,
                data: 'Id',
                render: function (data, type, full, row) {
                    var editar = baseUrl('Pessoa/IndexEditar?id=' + data + '&tipoPessoa=' + full.TipoPessoa);
                    return '<a class="btn btn-info btn-sm fa fa-pencil-square-o" data-toggle="tooltip" title="Alterar" href="' + editar + '">Editar</a> ' +                        
                           '<a class="btn btn-danger btn-sm fa fa-trash-o btn-lg"  title="Excluir" id="btnExcluir" data-id="' + full.Id + '" data-tipo-pessoa="' + full.TipoPessoa + '" data-toggle="modal" data-target="#myModal">Excluir</a> ';
                }
            },
            {
                targets: 0,
                data: 'TipoPessoa',
                render: function (data) {
                    if (data === 'PF')
                        return 'PF';
                    return "PJ";
                }
            },            
            ],
            columns: [                    
                    { data: 'TipoPessoa' },
                    { data: 'NumeroDocumento' },
                    { data: 'Nome' },
                    { data: 'Logradouro' },
                    { data: 'Cep' },
                    { data: 'Bairro' },
                    { data: 'Cidade' },
                    { data: 'UF' },
                    { data: 'Id' }
            ],
        });
    }

    PesquisaClientesDatatable();
   
    
    $('#div_valorDocumento').hide();
    $('#div_valorNome').hide();
    $("#tipodePesquisa").prop('disabled', true);

    $('#tipodePessoa').on('change', function () {
        if ($(this).val()!=='') {
            $("#tipodePesquisa").prop('disabled', false);

            if ($(this).val() === 'PF')
                $('#documento').mask("999.999.999-99");
            else
                $('#documento').mask('99.999.999/9999-99');
        }
        
    });


    $('#documento').on('blur', function () {

        if ($('#tipodePesquisa').val() !== '' && $('#tipodePessoa').val() != '') {                        
            PesquisaClientesDatatable();
        }
    });

    $('#nome').on('blur',function () {

        if ($('#tipodePesquisa').val() !== '' &&  $('#tipodePessoa').val() != '') {
            PesquisaClientesDatatable();
        }
    });    

    function LimparValorPesquisa() {
        $('#nome').val('');
        $('#documento').val('');        
    }
    $("#tipodePesquisa").on('change',function () {
        var tipo = $("#tipodePesquisa").val();
        if (tipo !== '') {
            PesquisaClientesDatatable();
            if (tipo === "1" && $('#tipodePessoa').val() == 'PF') {
                $('#div_valorDocumento').show();
                $('#documento').mask("999.999.999-99");
                $('#div_valorNome').hide();              
                LimparValorPesquisa();
            } else if (tipo === "1" && $('#tipodePessoa').val() == 'PJ') {
                $('#div_valorDocumento').show();
                $('#documento').mask('99.999.999/9999-99');
                $('#div_valorNome').hide();
                LimparValorPesquisa();
            } else if (tipo === "2") {
                $('#div_valorDocumento').hide();
                $('#div_valorNome').show();
                LimparValorPesquisa();
            }
        }
    });

    function centerModal() {
        $(this).css('display', 'block');
        var $dialog = $(this).find(".modal-dialog");
        var offset = ($(window).height() - $dialog.height()) / 2;
        // Center modal vertically in window
        $dialog.css("margin-top", offset);
    }

    $('#confirme').click(function () {
        $.ajax({
            url: baseUrl('Pessoa/Excluir'),
            type: "GET",
            dataType: "json",
            data: { 'id': Id, 'tipoPessoa': tipoPessoa },
            success: function () {
                $(window.document.location).attr('href', baseUrl('Pessoa/Index'));
            }
        });
    });

    $('#datatable-clientes').delegate('#btnExcluir', 'click', function () {
        Id = $(this).attr('data-id');
        tipoPessoa = $(this).attr('data-tipo-pessoa');
        $('.modal').on('show.bs.modal', centerModal);
        $(window).on("resize", function () {
            $('.modal:visible').each(centerModal);
        });
    });

    $('#btnNovoCliente').click(function () {
        var url = $('#RedirectToNovoCliente').val();
        window.location.href = url;
    });
})($ || jquery);