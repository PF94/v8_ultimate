//made by zor lol

package printzor{
 function fxdtsbrickdata::onplant(%this,%obj){
  parent::onplant(%this,%obj);
  schedule(66,0,printcheckzor,%obj);
 }
 function servercmdsetprint(%client,%id){
  %client.lastprint_[%client.printbrick.getdatablock()]=%id;
  parent::servercmdsetprint(%client,%id);
 }
};
activatepackage(printzor);

function printcheckzor(%obj){
 if(isobject(%obj)){
 if(%obj.getdatablock().hasprint && isobject(%obj.client))
  %obj.setprint(%obj.client.lastprint_[%obj.getdatablock()]);
 }
}