package com.example.laundrygest_android

import android.content.Context
import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import androidx.transition.Visibility
import com.example.laundrygest_android.data.CollectionDto
import com.example.laundrygest_android.tools.DateParser
import java.text.SimpleDateFormat
import java.time.OffsetDateTime
import java.time.format.DateTimeFormatter
import java.util.Date
import java.util.Locale

class recyclerViewAdapter(val list: List<CollectionDto>) :
    RecyclerView.Adapter<recyclerViewAdapter.ViewHolder>() {
    lateinit var context: Context

    class ViewHolder(val view: View) : RecyclerView.ViewHolder(view) {
        val numCollection = view.findViewById<TextView>(R.id.collection_code_text)
        val dateCollection = view.findViewById<TextView>(R.id.collection_date_text)
        val priceCollection = view.findViewById<TextView>(R.id.due_total_text)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val layout = LayoutInflater.from(parent.context)
        this.context = parent.context
        return ViewHolder(layout.inflate(R.layout.cardview, parent, false))
    }

    override fun getItemCount() = list.size

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.numCollection.append(list[position].number.toString())
        holder.dateCollection.append(DateParser.formatDate(list[position].dueDate.toString()))
        var dueTotal = list[position].dueTotal
        if(dueTotal!=null) {
            holder.priceCollection.append(String.format("%.2f€", dueTotal))
        }else{
            holder.priceCollection.visibility = View.INVISIBLE
        }
        holder.itemView.setOnClickListener {
            var intent = Intent(context, CollectionDetailActivity::class.java)
            intent.putExtra("collection", list[position])
            context.startActivity(intent)
        }
    }

}