$(function () {
    var sidebar = $('[data-sidebar]');
    var sidebarToggle = $('[data-sidebar-toggle]');

    $("textarea").each(function () {
        this.setAttribute("style", "height:" + (this.scrollHeight) + "px;overflow-y:hidden;");
    }).on("input", function () {
        this.style.height = "auto";
        this.style.height = (this.scrollHeight) + "px";
    });

    sidebarToggle.click(function(){
        if(sidebar.hasClass('visible'))
            sidebar.removeClass('visible');
        else
            sidebar.addClass('visible');
    });
});