"""Interactive dashboard comparing LLM models on F# experiments.

Usage:
    python scripts/visualize_fsharp_multimodel.py

Opens a browser with an interactive Plotly dashboard.
"""

import pandas as pd
import plotly.express as px
import plotly.graph_objects as go
from plotly.subplots import make_subplots
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
CSV = ROOT / "results.csv"


def load():
    df = pd.read_csv(CSV, sep=";")
    df["start_time"] = pd.to_datetime(df["start_time"])
    df["end_time"] = pd.to_datetime(df["end_time"])
    df["duration_s"] = (df["end_time"] - df["start_time"]).dt.total_seconds()
    df["tokens"] = pd.to_numeric(df["tokens"], errors="coerce")
    df["number_of_builds_test_runs_needed"] = pd.to_numeric(
        df["number_of_builds_test_runs_needed"], errors="coerce"
    )
    df["attempt"] = df["attempt"].astype(str)
    df = df[df["language"] == "fsharp"]
    return df


def build_dashboard(df):
    fig = make_subplots(
        rows=2, cols=2,
        subplot_titles=(
            "Duration (seconds) by Model",
            "Build/Test Runs Needed by Model",
            "Tokens by Model",
            "Duration vs Build/Test Runs",
        ),
        vertical_spacing=0.12,
        horizontal_spacing=0.1,
    )

    colors = px.colors.qualitative.Set2
    models = df["model"].unique()
    color_map = {m: colors[i % len(colors)] for i, m in enumerate(models)}

    for model in models:
        sub = df[df["model"] == model]
        c = color_map[model]
        hover = sub.apply(
            lambda r: f"Model: {r['model']}<br>Lang: {r['language']}<br>Attempt: {r['attempt']}<br>Duration: {r['duration_s']:.1f}s<br>Builds: {r['number_of_builds_test_runs_needed']}",
            axis=1,
        )

        fig.add_trace(go.Box(y=sub["duration_s"], name=model, marker_color=c, legendgroup=model, showlegend=True), row=1, col=1)
        fig.add_trace(go.Box(y=sub["number_of_builds_test_runs_needed"], name=model, marker_color=c, legendgroup=model, showlegend=False), row=1, col=2)
        fig.add_trace(go.Box(y=sub["tokens"], name=model, marker_color=c, legendgroup=model, showlegend=False), row=2, col=1)
        fig.add_trace(go.Scatter(
            x=sub["number_of_builds_test_runs_needed"],
            y=sub["duration_s"],
            mode="markers",
            marker=dict(color=c, size=10),
            name=model,
            legendgroup=model,
            showlegend=False,
            text=hover,
            hoverinfo="text",
        ), row=2, col=2)

    fig.update_yaxes(title_text="Seconds", row=1, col=1)
    fig.update_yaxes(title_text="Runs", row=1, col=2)
    fig.update_yaxes(title_text="Tokens", row=2, col=1)
    fig.update_yaxes(title_text="Duration (s)", row=2, col=2)
    fig.update_xaxes(title_text="Build/Test Runs", row=2, col=2)

    fig.update_layout(
        title_text="F# Experiment Results — Multi-Model Comparison",
        height=800,
        template="plotly_white",
    )
    return fig


def main():
    df = load()
    print(f"Loaded {len(df)} F# rows: {df['model'].nunique()} models")
    print(df.groupby(["model", "language"])[["duration_s", "number_of_builds_test_runs_needed"]].mean().round(1).to_string())
    print()
    fig = build_dashboard(df)
    fig.show()


if __name__ == "__main__":
    main()
