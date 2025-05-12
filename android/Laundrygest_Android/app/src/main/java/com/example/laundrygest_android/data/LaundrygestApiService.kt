package com.example.laundrygest_android.data

import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Path
import retrofit2.http.Query

interface LaundrygestApiService {
    @GET("client_mobile")
    suspend fun getClientLogin(
        @Query("user") user: String,
        @Query("password") password: String
    ): Response<Client>

    @GET("collections/{code}")
    suspend fun getCollectionsClient(@Path("code") code: Int): Response<List<CollectionDto>>
}