var url = "http://localhost:5010/api/"

var tokenPage = new URL(window.location.href).searchParams.get("tokenPage");

async function drawProjetos(listProjetos){
    removeElement("loading-spin");
    if(listProjetos.length > 0){
        listProjetos.forEach(element => {
            drawCardProjeto(element);
        });;
    }else{
        drawErroProjeto();
    }
}

function drawCardProjeto(obj){
    var divProjetos = document.getElementById("div-projetos");
    var card = divProjetos.innerHTML;
    card += '<div class="card pt-2 border border-secondary card-space" style="width: 18rem;" id="'+obj.id+'">';
    card += '  <img src="'+obj.imgUrl+'" class="card-img-top" alt="'+obj.descricao+'">';
    card += '  <div class="card-body">';
    card += '      <h5 class="card-title">'+obj.nome+'</h5>';
    card += '      <p class="card-text">'+obj.descricao+'</p>';
    card += '      <a href="'+obj.url+'" class="btn btn-primary">Ir para projeto</a>';
    card += '  </div>';
    card += '</div>';

    divProjetos.innerHTML=card;
}

function drawErroProjeto(){
    var divProjetos = document.getElementById("div-projetos");
    var card = "";
    card += '<span class="icon-alert text-center">'
    card += '   <i class="fas fa-exclamation-circle"></i>'    
    card += '</span>';
    card += '<h3 class="text-center">Não existem projetos para exibir, por favor, tente novamente mais tarde.</h3>';

    divProjetos.innerHTML=card;
}

$("body").ready(function(){
    //console.log(md5("teste"));
    getProjetos();
});

async function getProjetos(){
    $.ajax({ 
        url: url+"Projeto/getByTokenPage?tokenPage="+tokenPage,
        method: 'GET',
        crossDomain: true,
        crossOrigin: true,
        success: function (response) {
            drawProjetos(response);
        },
        error: function (error) {
            drawErroProjeto();
            console.log(error);
        }
    });
}

function removeElement(id){
    var element = document.getElementById(id);

    element.remove();
}
