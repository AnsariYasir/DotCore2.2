function getQueryStringByParameter(url, paraName) {

    const urlParam = new URLSearchParams(url);
    return urlParam.get(paraName);
    
}

function showMessage(result)
{
    if (result.Status === 1)//Success
    {
        $('#divMessage').addClass('alert-success');
        //window.location.href = "../Account/Login";
    }
    else if(result.Status === 2)
    {
        $('#divMessage').addClass('alert-danger');                
    }

    $('#divMessageInner').text(result.Message)
    $("#divMessage").delay(5000).addClass("in").fadeOut(3000);
}
