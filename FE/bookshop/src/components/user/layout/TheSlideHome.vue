<template>
  <!-- <div class="slideshow-container">
    <div
      class="slideshow"
      @mousedown="handleMouseDown"
      @mousemove="handleMouseMove"
      @mouseup="handleMouseUp"
    >
      <div
        v-for="(slide, index) in slides"
        :key="index"
        class="slide"
        :style="{ opacity: index === currentSlideIndex ? 1 : 0 }"
      >
        <img :src="slide.image" alt="Slide Image" class="slide-image" />
      </div>
    </div>
  </div> -->
  <swiper
    style="height: 100%"
    :autoplay="{ delay: 3000, disableOnInteraction: false }"
    :modules="modules"
    :slides-per-view="1"
    :pagination="{ clickable: true }"
  >
    <swiper-slide
      :pagination="{ clickable: true }"
      v-for="(slide, index) in slides"
      :key="index"
    >
      <img :src="slide.image" class="slide-image" />
    </swiper-slide>
  </swiper>
</template>

<script>
// import Swiper core and required modules
import { Pagination, Scrollbar, Autoplay } from "swiper/modules";

// Import Swiper Vue.js components
import { Swiper, SwiperSlide } from "swiper/vue";

// Import Swiper styles
import "swiper/css";
import "swiper/css/navigation";
import "swiper/css/pagination";
import "swiper/css/scrollbar";

// Import Swiper styles
export default {
  components: {
    Swiper,
    SwiperSlide,
  },
  setup() {
    const onSwiper = (swiper) => {
      console.log(swiper);
    };
    const onSlideChange = () => {
      console.log("slide change");
    };
    const slides = [
      { image: require("@/assets/banner/slider_6.jpg") },
      { image: require("@/assets/banner/slider_7.jpg") },
      { image: require("@/assets/banner/slider_1.jpg") },
      // Add more slides as needed
    ];
    return {
      onSwiper,
      onSlideChange,
      slides,
      modules: [Autoplay, Pagination, Scrollbar],
    };
  },
};
</script>

<style>
.slideshow-container {
  position: relative;
  width: 100%;
  max-width: 100%;
  height: 100%;
  overflow: hidden;
  border-radius: 8px;
}

.slideshow {
  position: relative;
  width: 100%;
  height: 100%; /* Điều chỉnh chiều cao của slideshow */
}

.slide {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  opacity: 0;
  transition: opacity 1s ease;
}

.slide-image {
  width: 100%;
  height: 447px;
  background-size: cover; /* Đảm bảo ảnh vừa khít với kích thước của slide */
}

.slideshow {
  cursor: pointer;
  transition: transform 0.3s ease;
}

.slideshow.dragging {
  transition: none;
}

.slideshow.dragging.active {
  cursor: grabbing;
}
</style>
