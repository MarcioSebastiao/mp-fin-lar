--
-- PostgreSQL database dump
--

\restrict 8CiBUMXkrEZhFGVoBcPmkhqetJrpg6i0KcAGCIIf7icKEA5jreNiO4m3KZyLxzA

-- Dumped from database version 16.12
-- Dumped by pg_dump version 16.12

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Categorias; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Categorias" (
    "Id" uuid NOT NULL,
    "Descricao" character varying(400) NOT NULL,
    "Finalidade" text NOT NULL,
    "DataCriacao" timestamp with time zone DEFAULT '-infinity'::timestamp with time zone NOT NULL
);


ALTER TABLE public."Categorias" OWNER TO postgres;

--
-- Name: Pessoas; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Pessoas" (
    "Id" uuid NOT NULL,
    "Nome" character varying(200) NOT NULL,
    "Idade" integer NOT NULL,
    "DataCriacao" timestamp with time zone DEFAULT '-infinity'::timestamp with time zone NOT NULL
);


ALTER TABLE public."Pessoas" OWNER TO postgres;

--
-- Name: Transacoes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Transacoes" (
    "Id" uuid NOT NULL,
    "Descricao" character varying(400) NOT NULL,
    "Valor" numeric(18,2) NOT NULL,
    "Tipo" text NOT NULL,
    "CategoriaId" uuid NOT NULL,
    "PessoaId" uuid NOT NULL,
    "DataCriacao" timestamp with time zone DEFAULT '-infinity'::timestamp with time zone NOT NULL
);


ALTER TABLE public."Transacoes" OWNER TO postgres;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Data for Name: Categorias; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Categorias" ("Id", "Descricao", "Finalidade", "DataCriacao") FROM stdin;
019c7142-0694-7cdb-9564-93811944fb9f	Compras	Despesa	2026-02-18 14:57:58.420472+00
019c7142-33b8-7f6d-b4c3-db0971c249c8	Investimentos	Receita	2026-02-18 14:58:09.976248+00
019c7142-58c3-7323-b1cd-5c9cd7bdd64b	Vendas	Receita	2026-02-18 14:58:19.459117+00
019c7143-0cf6-7c19-a6cc-f21d6e6f0595	Serviços	Ambas	2026-02-18 14:59:05.590609+00
\.


--
-- Data for Name: Pessoas; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Pessoas" ("Id", "Nome", "Idade", "DataCriacao") FROM stdin;
019c7141-9917-70d1-8dfa-2085c4fd40e7	Jane Doe	28	2026-02-18 14:57:30.391889+00
019c7141-c1f1-737c-9cb1-afcde064954e	John Doe	34	2026-02-18 14:57:40.849121+00
\.


--
-- Data for Name: Transacoes; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Transacoes" ("Id", "Descricao", "Valor", "Tipo", "CategoriaId", "PessoaId", "DataCriacao") FROM stdin;
019c7144-2217-795c-897d-ae008258de1a	Venda Produto A	1500.00	Receita	019c7142-58c3-7323-b1cd-5c9cd7bdd64b	019c7141-c1f1-737c-9cb1-afcde064954e	2026-02-18 15:00:16.535346+00
019c7144-9a05-73a1-a308-5bfc42499c1a	Compra de Matéria-Prima	780.00	Despesa	019c7142-0694-7cdb-9564-93811944fb9f	019c7141-c1f1-737c-9cb1-afcde064954e	2026-02-18 15:00:47.237293+00
019c7145-9ebe-7682-a9db-56b416cd64d1	Pagamento de Serviço de Limpeza	450.00	Despesa	019c7143-0cf6-7c19-a6cc-f21d6e6f0595	019c7141-c1f1-737c-9cb1-afcde064954e	2026-02-18 15:01:53.982849+00
019c7146-11ae-7897-a248-8f7257066940	\tDividendos Recebidos	480.00	Receita	019c7142-33b8-7f6d-b4c3-db0971c249c8	019c7141-c1f1-737c-9cb1-afcde064954e	2026-02-18 15:02:23.406097+00
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20260213235857_CriarEstruturaInicial	10.0.3
20260214000248_AdicionarTabelaCategoria	10.0.3
20260214154722_AdicionarTabelaTransacoes	10.0.3
20260217212121_AdicionarDataCriacao	10.0.3
\.


--
-- Name: Categorias PK_Categorias; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Categorias"
    ADD CONSTRAINT "PK_Categorias" PRIMARY KEY ("Id");


--
-- Name: Pessoas PK_Pessoas; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pessoas"
    ADD CONSTRAINT "PK_Pessoas" PRIMARY KEY ("Id");


--
-- Name: Transacoes PK_Transacoes; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Transacoes"
    ADD CONSTRAINT "PK_Transacoes" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_Transacoes_CategoriaId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Transacoes_CategoriaId" ON public."Transacoes" USING btree ("CategoriaId");


--
-- Name: IX_Transacoes_PessoaId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Transacoes_PessoaId" ON public."Transacoes" USING btree ("PessoaId");


--
-- Name: Transacoes FK_Transacoes_Categorias_CategoriaId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Transacoes"
    ADD CONSTRAINT "FK_Transacoes_Categorias_CategoriaId" FOREIGN KEY ("CategoriaId") REFERENCES public."Categorias"("Id") ON DELETE RESTRICT;


--
-- Name: Transacoes FK_Transacoes_Pessoas_PessoaId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Transacoes"
    ADD CONSTRAINT "FK_Transacoes_Pessoas_PessoaId" FOREIGN KEY ("PessoaId") REFERENCES public."Pessoas"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict 8CiBUMXkrEZhFGVoBcPmkhqetJrpg6i0KcAGCIIf7icKEA5jreNiO4m3KZyLxzA

