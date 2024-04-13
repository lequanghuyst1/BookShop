<template>
   <div class="slideshow-container">
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
  </div>
</template>

<script>
export default {
  name: "SlideHome",
  data() {
    return {
      slides: [
        { image: require("@/assets/banner/slider_1.jpg") },
        { image: require("@/assets/banner/slider_2.jpg") },
        { image: require("@/assets/banner/slider_3.jpg") },
        { image: require("@/assets/banner/slider_4.jpg") },
        { image: require("@/assets/banner/slider_5.jpg") },
        // Add more slides as needed
      ],
      currentSlideIndex: 0,
      slideInterval: null,
      isDragging: false,
      startX: 0,
      endX: 0,
    };
  },
  mounted() {
    this.startSlideShow();
  },
  methods: {
    startSlideShow() {
      this.slideInterval = setInterval(() => {
        this.nextSlide();
      }, 4000); // Thời gian chuyển đổi giữa các slide (3 giây)
    },
    nextSlide() {
      this.currentSlideIndex =
        (this.currentSlideIndex + 1) % this.slides.length;
    },
    prevSlide() {
      if (this.currentSlideIndex === 0) {
        this.currentSlideIndex = this.slides.length - 1;
      } else {
        this.currentSlideIndex--;
      }
    },
    handleMouseDown(event) {
      this.isDragging = true;
      this.startX = event.clientX;
    },
    handleMouseMove(event) {
      if (this.isDragging) {
        this.endX = event.clientX;
      }
    },
    handleMouseUp() {
      if (this.isDragging) {
        this.isDragging = false;
        const deltaX = this.endX - this.startX;
        if (deltaX > 0) {
          this.prevSlide();
        } else {
          this.nextSlide();
        }
      }
    },
  },
  beforeUnmount() {
    clearInterval(this.slideInterval);
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
  height: 100%;
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
