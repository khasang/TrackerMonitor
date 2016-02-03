    jQuery(document).ready(function()
    {
	    $.backstretch("/Content/img/backgrounds/1.jpg");
	    
	    $('#top-navbar-1').on('shown.bs.collapse', function(){
	    	$.backstretch("resize");
	    });
	    $('#top-navbar-1').on('hidden.bs.collapse', function(){
	    	$.backstretch("resize");
	    });
    });
    