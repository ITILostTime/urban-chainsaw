<template>
    <section class="col-md-8" id="carrousel">
        <slider animation="fade"
            width="100%"
            height="400px"
            :init-index="0"
            :auto="true"
            :interval="4000"
            >
            <slider-item v-for="(i, index) in itemList" :key="index">
                <article
                    <img :src="'/dist/'+i.pictureName" alt="screen"/>
                    <h1>{{i.title}}</h1>
                </article>
            </slider-item>
        </slider>
    </section>
</template>
<script>
import { Slider, SliderItem } from 'vue-easy-slider'
    export default {
        data () {
            return {
                itemList: [
                    { title: 'Incarnez Astrid !', pictureName: 'screen1.png' },
                    { title: 'Résolvez des énigmes !', pictureName: 'screen2.png' },
                    { title: 'Explorez la Ville !', pictureName: 'screen3.png' },
                    { title: 'Maîtrisez votre destin !', pictureName: 'screen4.png' },
                ]
            }
        },
        components: {
            Slider,
            SliderItem
            },
        methods: {
            test () {
                console.log(1)
            },
        },
    }
</script>
<style lang="less">
.slider
{
    height:634px;
    max-height:634px;
    max-width:891px;
}
.slider button:focus 
{
    outline:none;
}
.slider button:active
{
    outline:none;
}
#carrousel
{
    max-width:562px;
    margin-right:0px;
    margin-bottom:50px;
}

#carrousel:before
{
    background:blue;
    position:absolute;
    content:" ";
    bottom:-45px;
    right:0px;
    width:120%;
    height:40px;
    background:url('../../dist/img/poutrelle_h.png') top left repeat-x;
    z-index:800;
}
@media (max-width: 1200px) 
{
    #carrousel
    {
        margin-left:18px;
        
    }
    #carrousel:before
    {
        left:5px;
        width:100%;
    }
}

#carrousel article
{
    position:relative;
    height:634px;
    max-height:634px;
    
}
#carrousel img
{
    width: auto; 
    max-height:400px;
    position:relative;
    z-index:0;
}
#carrousel h1
{
    color:white;
    padding:10px;
    padding-bottom:35px;
    margin:0px;
    position:absolute;
    bottom:0px;
    z-index:1;
    width:100%;
    text-shadow: 4px 4px 6px black;
    //background-color:pink;
    //background: -moz-linear-gradient(top, rgba(0,0,0,0) 0%, rgba(0,0,0,0.6) 77%, rgba(0,0,0,0.6) 100%); /* FF3.6-15 */
    //background: -webkit-linear-gradient(top, rgba(0,0,0,0) 0%,rgba(0,0,0,0.6) 77%,rgba(0,0,0,0.6) 100%); /* Chrome10-25,Safari5.1-6 */
    //background: linear-gradient(to bottom, rgba(0,0,0,0) 0%,rgba(0,0,0,0.6) 77%,rgba(0,0,0,0.6) 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
    //filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#00000000', endColorstr='#99000000',GradientType=0 ); /* IE6-9 */
}
</style>