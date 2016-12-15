$('.nav li:has(ul)').addClass('menu-item-has-children');
$('.nav li a[href="' + location.pathname + '"]').parent('li').addClass('current-menu-item');
$('.nav li:has(li.current-menu-item)').addClass('current-menu-parent');

$('.category li:has(ul)').addClass('menu-item-has-children');
$('.category li a[href="' + location.pathname + '"]').parent('li').addClass('current-menu-item');
$('.category li:has(li.current-menu-item)').addClass('current-menu-parent');
$('.category li a[href="' + location.pathname + '"]').addClass('current-menu-item');