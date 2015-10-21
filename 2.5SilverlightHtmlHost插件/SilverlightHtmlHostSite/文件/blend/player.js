///////////////////////////////////////////////////////////////////////////////
//
//  ExtendedPlayer
//
//  这将扩展 Player 基类，您可以在此处重写 Player 基类
//  成员功能或者添加其他的播放器功能。
//
///////////////////////////////////////////////////////////////////////////////
Type.registerNamespace('ExtendedPlayer');

ExtendedPlayer.Player = function(domElement) {
    ExtendedPlayer.Player.initializeBase(this, [domElement]);  
}
ExtendedPlayer.Player.prototype =  {
}
ExtendedPlayer.Player.registerClass('ExtendedPlayer.Player', EePlayer.Player);

