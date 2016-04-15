$(function(){
	$(".frame").click(function(){
        $(this).toggleClass("blue");
        $(this).children().first().slideToggle();
    });
    
    $(".destination, .source, .ethertype, #destinationSpan, #sourceSpan, #ethertypeSpan").hover(function(){
        $(this).toggleClass("highlight");
    });
    
    //Destination Span Mouse in/ mouse out handler
    $("#destinationSpan").hover(function(){
        $("#info").html("Destination MAC Address: <b>" +$(this).html() +"</b> 6 bytes");
    },function(){
        $("#info").html("");
    });
    
    //Source Span Mouse in/ mouse out handler
    $("#sourceSpan").hover(function(){
        $("#info").html("Source MAC Address: <b>" +$(this).html() +"</b> 6 bytes");
    },function(){
        $("#info").html("");
    });
    
    //ethertype Span Mouse in/ mouse out handler
    $("#ethertypeSpan").hover(function(){
        $("#info").html("EtherType: <b>" +$(this).html() +"</b> 6 bytes");
    },function(){
        $("#info").html("");
    });
    
    //"Hide Footer Button"
    $("#hideFooter").click(function(){
       $("#footer").slideToggle();
    });
	  
}
);


    //Takes in the Node ID and graph object, and fills in the footer with relevant information
function fillFooter(ID,graph){
    $(function(){
      for(i=0;i<graph.nodes.length;i++){
          if(ID == graph.nodes[i].id){
              console.log(graph.nodes[i].frameData);
              buildFooterList(graph.nodes[i].frameData);
              buildPacketFooter(graph.nodes[i].frameData);
          }
      }
        
    });
}

//Take in the "FrameData" Object from 
function buildFooterList(data){
    $(".ethernet-list").children().find(".destination").html("Destination: "+data.destination);
    $(".ethernet-list").children().find(".source").html("Source: "+data.source);
    $(".ethernet-list").children().find(".ethertype").html("Type: "+data.ethertype);
}

function buildPacketFooter(data){
    var rawData = data.data;
    rawData = rawData.split(" ");
    
    //Empty the packet display
    $("#destinationSpan").html("");
    $("#sourceSpan").html("");
    $("#ethertypeSpan").html("");
    $("#rawDataSpan").html("");
    for(i=0;i<rawData.length;i++){
        if(i%16 == 0 && i>14){
            $("#rawDataSpan").html( $("#rawDataSpan").html() + "\n");
        }
        if(i<6){
            $("#destinationSpan").html( $("#destinationSpan").html() + rawData[i]+" ");
        }
        else if(i<12){
            $("#sourceSpan").html( $("#sourceSpan").html() + rawData[i]+" ");
        }
        else if(i<14){
            $("#ethertypeSpan").html( $("#ethertypeSpan").html() + rawData[i]+" ");
        }
        else{
            $("#rawDataSpan").html( $("#rawDataSpan").html() + rawData[i]+" ");
        }
    }
}
