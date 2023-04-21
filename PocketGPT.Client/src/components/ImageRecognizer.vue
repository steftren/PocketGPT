<template>
  <div>
    <video ref="video" autoplay></video>
    <button @click="capture">Capture</button>
  </div>
</template>

<script>

export default {
  data() {
    return {
      imageStream: null,
    };
  },
  mounted() {
    navigator.mediaDevices.getUserMedia({ video: true })
      .then((stream) => {
        this.$refs.video.srcObject = stream;
        this.imageStream = stream;
      })
      .catch((err) => {
        console.error(`Could not get media stream: ${err}`);
      });
  },
  methods: {
    async capture() {
      const canvas = document.createElement('canvas');
      canvas.width = this.$refs.video.videoWidth;
      canvas.height = this.$refs.video.videoHeight;

      const ctx = canvas.getContext('2d');
      ctx.drawImage(this.$refs.video, 0, 0, canvas.width, canvas.height);

      const imageData = ctx.getImageData(0, 0, canvas.width, canvas.height);
      const correctedImageData = await OcrService.correctImage(imageData);

      const questions = await OcrService.recognizeText(correctedImageData);
      this.$emit('onRecognize', questions);
    },
  },
};
</script>
