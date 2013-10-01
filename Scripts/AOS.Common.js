$(function(){
	if($('.tabs li')[0]){
		$('.tabs li').click(function(){
			$(this).parent().find('li').removeClass("active");
			$(this).addClass('active');
			})
		});
	}
	if($('.slide-strip')[0]){
		$('.slide-strip').slideStrip({slide:true, slingBack:false});
	}
});