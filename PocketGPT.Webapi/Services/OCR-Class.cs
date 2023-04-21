import ocrad from 'ocrad.js';

class OcrService
{
    constructor() { }

    async recognizeText(imageData)
    {
        const text = ocrad(imageData.data, imageData.width, imageData.height);
        const questions = text.split('\n').filter((line) => line.trim().length > 0);
        return questions;
    }

    async correctImage(imageData)
    {
        // TODO: Implement image correction
        return imageData;
    }
}

const ocrService = new OcrService();
export default ocrService