using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace ChallengeChurnAnalytics.Services
{
    public class SentimentAnalysisService
    {
        // contexto do ML.NET usado para criar modelos de machine learning
        private readonly MLContext _mlContext;
        private ITransformer _model;

        // construtor que inicializa o contexto do ML.NET e carrega o modelo de sentimento
        public SentimentAnalysisService()
        {
            _mlContext = new MLContext();
            LoadModel();
        }

        // método responsável por carregar o modelo de sentimento se o arquivo existir
        private void LoadModel()
        {
            var modelPath = "MLModels/sentiment_model.zip";
            if (File.Exists(modelPath))
            {
                DataViewSchema modelSchema;
                // carrega o modelo a partir do caminho especificado e define o esquema do modelo
                _model = _mlContext.Model.Load(modelPath, out modelSchema);
            }
        }

        // método que prevê o sentimento de um texto fornecido
        public string PredictSentiment(string text)
        {
            // cria um mecanismo de previsão usando o modelo carregado
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
            var input = new SentimentData { Text = text };
            // realiza a previsão com base no texto fornecido e retorna "Positive" ou "Negative"
            var result = predictionEngine.Predict(input);
            return result.Prediction ? "Positive" : "Negative";
        }
    }

    // classe que representa os dados de entrada para o modelo de análise de sentimento
    public class SentimentData
    {
        [LoadColumn(0)]
        public string Text;

        [LoadColumn(1), ColumnName("Label")]
        public bool Sentiment;
    }

    // classe para a previsão do modelo que inclui o rótulo previsto
    public class SentimentPrediction : SentimentData
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction;
    }
}
