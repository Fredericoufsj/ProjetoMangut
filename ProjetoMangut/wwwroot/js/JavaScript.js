var ObjetoVenda = new Object();


ObjetoVenda.SalvarVenda = function (venda) {


    var qtd = $("#qtd_" + venda).val();
    var nome = $("#nome_" + venda).text();

    $.ajax({
        type: 'POST',
        url: "api/AdicionarProd",
        datatype: "JSON",
        ContentType: "application/json",
        cache: false,
        async: true,
        data: {
            "id": venda, "nome": nome, "qtd": qtd
        },

        success: function (data) {

        }
    });
}


ObjetoVenda.CarregarProdutos = function () {


    $.ajax({
        type: 'GET',
        url: "api/ListarProdutos",
        datatype: "JSON",
        ContentType: "application/json",
        cache: false,
        async: true,
        success: function (data) {

            var htmlConteudo = "";
            data.forEach(function (entry) {

                htmlConteudo += " <div class='col-xs-12 col-sm-4 col-md-4 col-lg-4'>";


                var idNome = "nome_" + entry.id;
                var idQtd = "qtd_" + entry.id;

                htmlConteudo += "<label id='" + idNome + "' > Produto: " + entry.nomeProduto + "</label></br>";

                htmlConteudo += "<img width='200' height='100' src='" + entry.url + "'/></br>";

                htmlConteudo += "<label>  Valor: " + entry.preco + "</label></br>";

                htmlConteudo += "<input type'number' id='" + idQtd + "'>";

                htmlConteudo += "<input type='button' onclick='ObjetoVenda.SalvarVenda(" + entry.id + ")' value ='Comprar'> </br> ";

                htmlConteudo += " </div>";


            });


            $("#DivVenda").html(htmlConteudo);


        }


    });

};


$(function () {

    ObjetoVenda.CarregarProdutos();
});