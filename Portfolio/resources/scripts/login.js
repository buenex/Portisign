var url = "http://192.168.0.117:5010/api/"
var login_panel;
var esqueci_panel;
var cadastro_panel;

function showPanel(panel){
    login_panel.setAttribute("hidden","true");
    esqueci_panel.setAttribute("hidden","true");
    cadastro_panel.setAttribute("hidden","true");

    switch(panel){
        case 'login':
            login_panel.removeAttribute("hidden");
            break;
        case 'esqueci':
            esqueci_panel.removeAttribute("hidden");
            break;
        case 'cadastro':
            cadastro_panel.removeAttribute("hidden");
            break;
    }
}

function move(panel,right,show) {
    var element = getPanelByName(panel);
    var left = parseInt(element.style.marginLeft);

    if(right)
        left += 1+(left/20);
    else
        left -= 1+(left/20);
    element.style.marginLeft = left + '%';

    if (left <125 &&  left>25) {
        setTimeout(function(){move(panel,right,show)},10);
    }else{
        if(left<25){
            element.style.marginLeft="25%";
            return;
        }
        else if(left>=125){
            element.style.marginLeft="125%";
            showPanel(show);
            move(show,false,panel)
            return;
        }
    }
}

function getPanelByName(panel){
    switch(panel){
        case 'login':
            return login_panel
        case 'esqueci':
            return esqueci_panel
        case 'cadastro':
            return cadastro_panel
    }
}    

function checkRquiresToRegister(){
    var txtNome = document.getElementById("nome-cadastro");
    var txtEmail = document.getElementById("email-cadastro");
    var txtCpf = document.getElementById("cpf-cadastro");
    var txtUsuario = document.getElementById("usuario-cadastro");
    var txtSenha = document.getElementById("senha-cadastro");
    var txtConfirmaSenha = document.getElementById("confirma-senha-cadastro");
    var txtDicaSenha = document.getElementById("dica-senha-cadastro");
    var chkTermos = document.getElementById("termos-cadastro");

    var divergencias = "Por favor Atenda os requisitos a seguir: \n\n";

    if(txtNome.value.trim() == "")
        divergencias += "-Preencher campo Nome \n";

    if(txtEmail.value.trim() == "")
        divergencias += "-Preencher campo Email \n";

    if(txtUsuario.value.trim() == "")
        divergencias += "-Preencher campo Usuario \n";

    if(txtSenha.value.trim() == "")
        divergencias += "-Preencher campo Senha \n";

    if(txtConfirmaSenha.value.trim() == "")
        divergencias += "-Preencher campo Confirmar senha \n";

    if(txtDicaSenha.value == "")
        divergencias += "-Preencher campo Dica senha \n";

    if(!chkTermos.checked)
        divergencias += "-Preencher campo Termos de uso \n";

    if(txtSenha.value.length < 8)
        divergencias += "-Senha deve conter pelo menos 8 digitos \n";

    if(txtConfirmaSenha.value != txtSenha.value)
        divergencias += "-Senhas não conferem \n";

    var patternUpperCase = /[A-Z]/
    var patternSmallCase = /[a-z]/
    var patternNumber = /[0-9]/
    var patternSymbol = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/

    if(!patternUpperCase.test(txtSenha.value))
        divergencias += "-Senha deve conter letra maiúscula. \n";   

    if(!patternSmallCase.test(txtSenha.value))
        divergencias += "-Senha deve conter letra minúscula. \n"; 

    if(!patternNumber.test(txtSenha.value))
        divergencias += "-Senha deve conter número. \n"; 

    if(!patternSymbol.test(txtSenha.value))
        divergencias += "-Senha deve conter caractéres especiais. \n"; 


    if(divergencias != "Por favor Atenda os requisitos a seguir: \n\n"){
        alert(divergencias);
        return false;
    }else{
        return true;
    }        
}

function cadastrar(){
    if(checkRquiresToRegister()){
        var txtNome = document.getElementById("nome-cadastro");
        var txtEmail = document.getElementById("email-cadastro");
        var txtCpf = document.getElementById("cpf-cadastro");
        var txtUsuario = document.getElementById("usuario-cadastro");
        var txtSenha = document.getElementById("senha-cadastro");
        var txtConfirmaSenha = document.getElementById("confirma-senha-cadastro");
        var txtDicaSenha = document.getElementById("dica-senha-cadastro"); 

        var data = {
            id:0,
            pessoa:{
                id:0,
                nome:txtNome.value,
                cpf:txtCpf.value,
                email:txtEmail.value
            },
            usuario: txtUsuario.value,
            senha: md5(txtSenha.value),
            dicaSenha: txtDicaSenha.value,
            token: gerarToken()
        };

        $.ajax({ 
            url: url+"Usuario/insert",
            method: 'POST',
            crossDomain: true,
            crossOrigin: true,
            data: JSON.stringify(data) ,
            dataType: "json",
            headers:{
                "Access-Control-Allow-Origin":"*",
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            success: function (response) {
                alert(response?"Cadastrado com sucesso!": "Usuario ou email já cadastrado, tente um diferente")
                
            },
            error: function (error) {
                alert("Não foi possível realizar o login devido a problemas com o servidor")
            }
        });
    }
}

function login(){
    var txtUsuario = document.getElementById("usuario-login");
    var txtSenha = document.getElementById("senha-login");

    var data = {
        usuario: txtUsuario.value,
        senha: md5(txtSenha.value)
    }

    $.ajax({ 
        url: url+"Usuario/login",
        method: 'POST',
        crossDomain: true,
        crossOrigin: true,
        data: JSON.stringify(data) ,
        dataType: "json",
        headers:{
            "Access-Control-Allow-Origin":"*",
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (response) {
            alert(response?"Login realizado com sucesso": "Usuario ou senha incorretos")
        },
        error: function (error) {
            alert("Não foi possível realizar o login devido a problemas com o servidor")
        }
    });
}

function esqueci(){
    var txtEmail = document.getElementById("email-recuperacao").value;

    $.ajax({ 
        url: url+"Usuario/sendEmail?email="+txtEmail,
        method: 'GET',
        crossDomain: true,
        crossOrigin: true,
        success: function (response) {
            alert("Email enviado com sucesso!");
        },
        error: function (error) {
            alert("Não foi possível realizar o envio do email")
        }
    });
}

document.addEventListener("DOMContentLoaded", function(event) { 
    login_panel = document.getElementById('login-panel');
    login_panel.setAttribute("style","margin-left:25%!important");

    esqueci_panel = document.getElementById('esqueci-panel');
    esqueci_panel.setAttribute("style","margin-left:125%!important");

    cadastro_panel = document.getElementById('cadastro-panel');
    cadastro_panel.setAttribute("style","margin-left:125%!important");
});